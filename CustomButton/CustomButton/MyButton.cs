using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomButton
{
    internal class MyButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(Brushes.Pink, ClientRectangle);
            pevent.Graphics.FillEllipse(Brushes.Blue, ClientRectangle);

        }
    }
}
