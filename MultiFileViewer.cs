using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_App3
{
    //https://www.umayadia.com/CSNyumon/BEGIN11.htm
    //保存機能 追加実装 要
    public partial class MultiFileViewer : Form
    {
        public MultiFileViewer()
        {
            InitializeComponent();
        }


        private void MaltiViewer_Load(object sender, EventArgs e)
        {
            //フォーム読み込み時
            


            //WebView2のダウンロード機能を無効
            //webView21.EnsureCoreWebView2Async();
            //webView21.CoreWebView2.DownloadStarting += (ds, de) => de.Cancel = true;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lstFileName_DragEnter(object sender, DragEventArgs e)
        {
            //listItems ドラッグ＆ドロップ
            //読み込み処理

            //ドラッグされているものがファイルであるか確認します。
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //ファイルであればコピー可能であることを宣言します。
                e.Effect = DragDropEffects.Copy;
            }

        }

        private void lstFileName_DragDrop(object sender, DragEventArgs e)
        {
            //listItems ドラッグ＆ドロップ
            //読み込み処理

            //ドロップされたファイルのフルパスを取得します。
            string fileName = (e.Data.GetData(DataFormats.FileDrop) as string[])[0];
            //フルパスからファイル情報を生成してlstFileNameに格納します。
            listFileName.Items.Add(new System.IO.FileInfo(fileName));
            //保ボタンを有効化
            btnSave.Enabled = true;
        }

        private void lstFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //webViewer
            //listItem 選択表示

            if (listFileName.SelectedItems != null)
            {
                
                System.IO.FileInfo file = listFileName.SelectedItem as System.IO.FileInfo;
                
                //listName、選択Indexがnullの場合（listItemsをクリックしていない）
                if (file != null)
                {
                    webView21.Source = new Uri(file.FullName);
                    this.Text = this.Name + "-" + Application.ProductName + " - " + file.FullName;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //保存ボタン押下

            //保存ファイル形式指定
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = ".txt";

            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            


            if (dialog.ShowDialog() != DialogResult.OK)
            {
                //OK で閉じられた時以外何もしない
                return;
            }

            using (var write = new System.IO.StreamWriter(dialog.FileName)) 
            {
                foreach(System.IO.FileInfo fileInfo in listFileName.Items)
                {
                    //ファイル情報からフルパスに書き込む
                    write.WriteLine(fileInfo.Name);
                }
            }
            //保ボタンを有効化
            btnSave.Enabled = true;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //読み込みボタン押下

            //開くファイル指定
            OpenFileDialog dialog = new OpenFileDialog();

            //ファイル開く
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                //OK以外で閉じられたとき何もしない
                return;
            }

            //ファイル読み込み
            foreach (string line in System.IO.File.ReadAllLines(dialog.FileName))
            {

                //MessageBox.Show(line);
                //フルパスから情報生成してlistFileNameに格納
                //listFileName.Items.Add(new System.IO.FileInfo(dialog.FileName));
            }

            //listItemsに追加
            listFileName.Items.Add(new System.IO.FileInfo(dialog.FileName));
        }




    }

}
