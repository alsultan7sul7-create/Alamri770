using System;
using System.Windows.Forms;

namespace StudentPerformanceApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // إنشاء قاعدة البيانات إذا لم تكن موجودة
            DatabaseManager.InitializeDatabase();
            
            Application.Run(new LoginForm());
        }
    }
}