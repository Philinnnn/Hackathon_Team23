using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Frontend.Models;

namespace Frontend
{
    public partial class ResumeListWindow : Window
    {
        public ResumeListWindow(List<Resume> resumes)
        {
            InitializeComponent();
            LoadResumes(resumes);
        }

        private void LoadResumes(List<Resume> resumes)
        {
            ResumeListBox.Items.Clear();
            foreach (var resume in resumes)
            {
                string languageProficiency = resume.LanguageProficiency ?? "Not specified";  // Показать уровень языка

                ResumeListBox.Items.Add($"Phone: {resume.Phone}, Email: {resume.Email}, Languages: {languageProficiency}");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Foreground.ToString() == "#888888")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = new SolidColorBrush(Color.FromArgb(128, 136, 136, 136));
                textBox.Text = textBox.Name switch
                {
                    "SexBox" => "Sex",
                    "ExperienceBox" => "Experience",
                    "LanguageBox" => "Language Proficiency",
                    _ => textBox.Text
                };
            }
        }
    }
}
