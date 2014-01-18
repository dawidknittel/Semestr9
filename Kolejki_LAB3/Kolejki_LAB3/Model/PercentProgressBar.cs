using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kolejki_LAB3.Model
{
    public class PercentProgressBar : ProgressBar
    {
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x000F)
            {
                var flags = TextFormatFlags.VerticalCenter |
                            TextFormatFlags.HorizontalCenter |
                            TextFormatFlags.SingleLine |
                            TextFormatFlags.WordEllipsis;

                double percent = Math.Round(((double)this.Value / this.Maximum * 100), 2);

                if (percent <= 100)
                {
                    TextRenderer.DrawText(CreateGraphics(), percent + "%", Font, new Rectangle(0, 0, this.Width, this.Height), Color.Black, flags);
                }
            }
        }
    }
}
