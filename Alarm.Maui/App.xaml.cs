using Alarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Alarm.Maui
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        private readonly AlarmLocalDbContext _context;
        public App(AlarmLocalDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override void OnStart()
        {
            _context.Database.Migrate();
        }
    }
}