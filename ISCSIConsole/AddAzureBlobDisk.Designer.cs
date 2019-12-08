namespace ISCSIConsole
{
    partial class AddAzureBlobDisk
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAzureBlobDisk));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.txt_ConnectionString = new System.Windows.Forms.TextBox();
            this.txt_Container = new System.Windows.Forms.TextBox();
            this.txt_Blob = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.chk_ReadOnlyBlob = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(401, 109);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(152, 35);
            this.btn_Ok.TabIndex = 0;
            this.btn_Ok.Text = "Connect To Blob";
            this.btn_Ok.UseVisualStyleBackColor = true;
            // 
            // txt_ConnectionString
            // 
            this.txt_ConnectionString.Location = new System.Drawing.Point(115, 21);
            this.txt_ConnectionString.Name = "txt_ConnectionString";
            this.txt_ConnectionString.Size = new System.Drawing.Size(438, 20);
            this.txt_ConnectionString.TabIndex = 1;
            this.txt_ConnectionString.Text = resources.GetString("txt_ConnectionString.Text");
            // 
            // txt_Container
            // 
            this.txt_Container.Location = new System.Drawing.Point(115, 47);
            this.txt_Container.Name = "txt_Container";
            this.txt_Container.Size = new System.Drawing.Size(438, 20);
            this.txt_Container.TabIndex = 2;
            this.txt_Container.Text = "mirror";
            // 
            // txt_Blob
            // 
            this.txt_Blob.Location = new System.Drawing.Point(115, 73);
            this.txt_Blob.Name = "txt_Blob";
            this.txt_Blob.Size = new System.Drawing.Size(438, 20);
            this.txt_Blob.TabIndex = 3;
            this.txt_Blob.Text = "DiskImg.img";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Connection String:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Container:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Blob:";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(342, 109);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(53, 35);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // chk_ReadOnlyBlob
            // 
            this.chk_ReadOnlyBlob.AutoSize = true;
            this.chk_ReadOnlyBlob.Location = new System.Drawing.Point(115, 109);
            this.chk_ReadOnlyBlob.Name = "chk_ReadOnlyBlob";
            this.chk_ReadOnlyBlob.Size = new System.Drawing.Size(125, 17);
            this.chk_ReadOnlyBlob.TabIndex = 8;
            this.chk_ReadOnlyBlob.Text = "Keep Blob ReadOnly";
            this.chk_ReadOnlyBlob.UseVisualStyleBackColor = true;
            // 
            // AddAzureBlobDisk
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(565, 157);
            this.Controls.Add(this.chk_ReadOnlyBlob);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Blob);
            this.Controls.Add(this.txt_Container);
            this.Controls.Add(this.txt_ConnectionString);
            this.Controls.Add(this.btn_Ok);
            this.Name = "AddAzureBlobDisk";
            this.Text = "AddAzureBlobDisk";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Cancel;
        public System.Windows.Forms.TextBox txt_ConnectionString;
        public System.Windows.Forms.TextBox txt_Container;
        public System.Windows.Forms.TextBox txt_Blob;
        public System.Windows.Forms.CheckBox chk_ReadOnlyBlob;
    }
}