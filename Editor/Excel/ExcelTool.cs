using Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

public class ExcelTool 
{
    /// <summary>
    /// Excel�ļ����·��
    /// </summary>
    public static string Excel_Path = Application.dataPath + "/ArtRes/Excel/";
    /// <summary>
    /// ���ݽṹ��洢λ��
    /// </summary>
    public static string Data_Class_Path = Application.dataPath + "/Scripts/ExcelData/DataClass/";
    /// <summary>
    /// ����������洢λ��
    /// </summary>
    public static string Data_Container_Path = Application.dataPath + "/Scripts/ExcelData/Container/";

    /// <summary>
    /// �������ݿ�ʼ������
    /// </summary>
    public static int Data_BeginIndex = 4;
    [MenuItem("GameTool/GenerateExcel")]
    private static void GenerateExcelInfo()
    {
        //��¼��ָ��·�������е�Excel�ļ� �������ɶ�Ӧ��3���ļ�
        DirectoryInfo dInfo = Directory.CreateDirectory(Excel_Path);
        //�õ������ļ���Ϣ �൱�ڵõ����е�Excel��
        //����Excel�� ���ݱ�����
        DataTableCollection tableConllection;
        FileInfo[] files = dInfo.GetFiles();
        for (int i = 0; i < files.Length; i++)
        {
            //ͨ����׺�� �޳�����Excel������ļ�
            if (files[i].Extension != ".xlsx" &&
                files[i].Extension != ".xls")
                continue;
            //��һ��Excel�ļ� ���õ����е��ļ�����
            using (FileStream fs = files[i].Open(FileMode.Open,FileAccess.Read))
            {
                IExcelDataReader excelReader = null;
                if (files[i].Extension == ".xls")
                {
                    //ʹ��.xls��ʽ ����ExcelReaderFactory.CreateBinaryReader(fs)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(fs);
                }
                else if (files[i].Extension != ".xlsx")
                {
                    //ʹ��.xlsx��ʽ ����ExcelReaderFactory.CreateOpenXmlReader(fs)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                }
                tableConllection = excelReader?.AsDataSet().Tables;
                fs.Close();
            }

            //�����ļ��е����б����Ϣ
            foreach (DataTable table in tableConllection)
            {
                //�������ݽṹ��
                GenarateExcelDataClass(table);
                //����������
                GenarateExcelContainer(table);
                //����2��������
                GenarateExcelBinary(table);
            }
        }
    }
    /// <summary>
    /// ����Excel���ݽṹ��ķ���
    /// </summary>
    /// <param name="table"></param>
    private static void GenarateExcelDataClass(DataTable table)
    {
        //�ֶ�����
        DataRow rowName = GetVariableNameRow(table);
        //�ֶ�������
        DataRow rowType = GetVariableTypeRow(table);
        //�ж�·���ļ����Ƿ���� ���û�� �ʹ����ļ���
        if (!Directory.Exists(Data_Class_Path))
            Directory.CreateDirectory(Data_Class_Path);

        string str = "public class " + table.TableName + "\n{\n";
        //���������ַ���ƴ��
        //�õ��ж�����
        for (int i = 0;i < table.Columns.Count; i++)
        {
            str += "    public " + rowType[i].ToString() + " " + rowName[i].ToString()+";\n";
        }

        str += "}";
        //��ƴ�Ӻõ��ַ��� �洢�� ָ�����ļ���ȥ
        File.WriteAllText(Data_Class_Path + table.TableName+".cs", str);

        //ˢ��project����
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// ��ȡ ������ ������
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    private static DataRow GetVariableNameRow(DataTable table)
    {
        return table.Rows[0];
    }
    /// <summary>
    /// ��ȡ ���������� ������
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    private static DataRow GetVariableTypeRow(DataTable table)
    {
        return table.Rows[1];
    }
    /// <summary>
    /// ��������������
    /// </summary>
    /// <param name="table"></param>
    private static  void GenarateExcelContainer(DataTable table)
    {
        //�õ���������
        int KeyIndex = GetMainKeyIndex(table);
        //�õ��ֶ�������
        DataRow rowType = GetVariableTypeRow(table);
        //û��·������·��
        if(!Directory.Exists(Data_Container_Path))
            Directory.CreateDirectory (Data_Container_Path);
        string str = "using System.Collections.Generic;\n";
        str += "public class " + table.TableName.ToString()+ "Container" + "\n{\n";
        str += "public Dictionary<" + rowType[KeyIndex].ToString() + "," + table.TableName.ToString() + ">";
        str += " dataDic = new Dictionary<" + rowType[KeyIndex].ToString() + "," + table.TableName.ToString() + ">();\n";
        str += "}";
        //д���ļ�
        File.WriteAllText(Data_Container_Path + table.TableName.ToString() + "Container.cs",str);
        //ˢ�½���
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// ��ȡ����������
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    private static int GetMainKeyIndex(DataTable table)
    {
        DataRow row = table.Rows[2];
        for (int i = 0;i < table.Constraints.Count;i++)
        {
            if (row[i].ToString()=="Key")
                return i;
        }
        return 0;
    }
    /// <summary>
    /// ����Excel����������
    /// </summary>
    /// <param name="table"></param>
    private static void GenarateExcelBinary(DataTable table)
    {
        //û��·������·��
        if(!Directory.Exists(BinaryDataManager.Data_Binary_Path))
            Directory.CreateDirectory(BinaryDataManager.Data_Binary_Path);
        //����һ���������ļ�д��
        using (FileStream fs = new FileStream(BinaryDataManager.Data_Binary_Path + table.TableName.ToString() + ".tang", FileMode.OpenOrCreate, FileAccess.Write))
        {
            //�洢Excel����Ķ����Ƶ���Ϣ
            //1.��Ҫ�洢������Ҫд�����е�����
            //-Data_BeginIndex��ԭ�� ǰData_BeginIndex�����ù��� ����������Ϣ
            fs.Write(BitConverter.GetBytes(table.Rows.Count- Data_BeginIndex),0,4);

            //2.�洢�����ı�����
            string keyName = GetVariableNameRow(table)[GetMainKeyIndex(table)].ToString();
            byte[] bytes =Encoding.UTF8.GetBytes(keyName);
            //2-1.�洢�����ַ����ֽ�����ĳ���
            fs.Write(BitConverter.GetBytes(bytes.Length),0,4);
            //2-2�洢�������ַ���
            fs.Write(bytes, 0, bytes.Length);
            
            //3.�洢����
            //3-1.����ÿһ��
            DataRow row;//��¼��
            DataRow rowType = GetVariableTypeRow(table);//��¼��
            for (int i = Data_BeginIndex; i < table.Rows.Count;i++)
            {
                row = table.Rows[i];
                //3-2����ÿһ��
                for (int j = 0;j<table.Columns.Count;j++)
                {
                    switch (rowType[j].ToString())
                    {
                        case "int":
                            fs.Write(BitConverter.GetBytes(int.Parse(row[j].ToString())),0,4);
                            break;
                        case "float":
                            fs.Write(BitConverter.GetBytes(float.Parse(row[j].ToString())), 0, 4);
                            break;
                        case "bool":
                            fs.Write(BitConverter.GetBytes(bool.Parse(row[j].ToString())), 0, 1);
                            break;
                        case "string":
                            bytes = Encoding.UTF8.GetBytes(row[j].ToString());
                            //��д�� �ַ����ֽ����� �ĳ���
                            fs.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
                            //��д�� �ַ���
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                    }
                }
            }
            fs.Close();
        }
        //ˢ��ҳ��
        AssetDatabase.Refresh();
    }
}
