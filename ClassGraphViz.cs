using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleListTarea
{
    internal class ClassGraphviz<Type>
    {
        private string _strPathGraph = "";
        private string _strPathGraphVizExe = "";
        public ClassGraphviz()
        {
            _strPathGraph = "c:\\Datos1";
            _strPathGraphVizExe = "C:\\Graphviz\\bin\\dot.exe";
        }
        public ClassGraphviz(string strSaveData, string strPathGraphVizExe)
        {
            _strPathGraph = strSaveData;
            _strPathGraphVizExe = strPathGraphVizExe;
        }
        public ClassGraphviz(string strPathFiles)
        {
            _strPathGraph = strPathFiles;
        }
        public void ExistFiles()
        {
            if (!Directory.Exists(_strPathGraph))
            {
                Directory.CreateDirectory(_strPathGraph);
                //MessageBox.Show("folder has been successfully created" + _strPathGraph);

            }
            if (!File.Exists(_strPathGraph + "\\Drawing.bat"))
            {
                //C:\Program Files\Graphviz\bin

                string strCodeBat = "@echo off\n" +
                    "cd " + _strPathGraph + "\n" +
                    "\"" +
                    _strPathGraphVizExe +
                    "\" -Tjpg DOTResult -o Drawing.jpg";

                StreamWriter miArchivoBat = new StreamWriter(_strPathGraph + "\\Drawing.Bat");
                miArchivoBat.Write(strCodeBat);
                miArchivoBat.Close();
                //MessageBox.Show("Has Been Created Drawing.Bat\n\n" + strCodeBat);
            }
        }

        public void CreatedDotFile(ClassNodeDoubleReference<Type> rootNode)
        {
            
            string strResult = "";
            strResult = strResult + "digraph Figura {";
            strResult = strResult + "\nRaíz->" + rootNode.ObjectType!.ToString() + ";";
            //MessageBox.Show(strResult);
            NodeTraversal(rootNode, ref strResult);
            strResult = strResult + "\n}";

            StreamWriter FileDot = new
            StreamWriter(_strPathGraph + "\\DOTResult");//Crear el file DOT
            FileDot.WriteLine(strResult);//EsCRIBE EN EL
            FileDot.Close();
        }
          
        private void NodeTraversal(ClassNodeDoubleReference<Type>? currentNode, ref string strResult)
        {
            if (currentNode != null)
            {
                if (currentNode.PointerBack != null)
                {
                    strResult = strResult + "\n" + "" + currentNode.ObjectType!.ToString() + "->" + currentNode.PointerBack.ObjectType!.ToString() + "" + " [color = blue]" + ";";
                }
                if (currentNode.PointerNext != null)
                {
                    strResult = strResult + "\n"  + "" + currentNode.ObjectType!.ToString() + "->" + currentNode.PointerNext.ObjectType!.ToString() + "" + " [color = red]" + ";";
                }

                NodeTraversal(currentNode.PointerBack, ref strResult);
                NodeTraversal(currentNode.PointerNext, ref strResult);
            }
        }
        public string ShowFilePath()
        {
            return "File Path Folder Graphviz: " + _strPathGraph;
        }

        public void ShowGraphviz()
        {
            if (!File.Exists(_strPathGraph + "\\Drawing.bat"))
            {
                return;
            }
            System.Diagnostics.Process.Start(_strPathGraph + "\\Drawing.bat").WaitForExit();
        }


        public Image ImageGraphJPG()
        {
            Image ImageGraph;
            FileStream FileImageJPG;
            try
            {
                FileImageJPG = new FileStream(_strPathGraph + "\\Drawing.jpg", FileMode.Open, FileAccess.Read);
            }
            catch (Exception e)
            {
                throw new Exception("Can't find or load image");
            }

            ImageGraph = System.Drawing.Bitmap.FromStream(FileImageJPG);
            FileImageJPG.Close();

            return ImageGraph;
        }

        public bool ExistDrawingJPG()
        {
            return File.Exists(_strPathGraph + "\\DOTResult");
        }
    }
}
