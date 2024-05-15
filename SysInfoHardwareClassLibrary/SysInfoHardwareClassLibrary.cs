using PluginBase;
using System.Management;

namespace SysInfoHardwareClassLibrary
{
    public class SysInfoHardwareCommand : ICommand
    {
        public string Name { get => "SysInfoHardware"; }

        public string Description { get => "Отображение информации о материнской плане, процессоре, видеокарте, биосе и адаптере интернета"; }

        public double Version { get => 1.0; }

        public List<string> Execute()
        {
            List<string> result = new()
            {
                $"Motherboard Manufacturer : {GetComponent("Win32_BaseBoard", "Manufacturer")}",
                $"Motherboard Model : {GetComponent("Win32_BaseBoard", "Product")}",
                $"CPU : {GetComponent("Win32_Processor", "Name")}",
                $"GPUs : {GetComponent("Win32_VideoController", "Name")}",
                $"BIOS Brand : {GetComponent("Win32_BIOS", "Manufacturer")}",
                $"BIOS Version : {GetComponent("Win32_BIOS", "Name")}",
                $"Network: : { GetComponent("Win32_NetworkAdapter", "Name")}",
            };

            return result;
        }

        private static string GetComponent(string hwclass, string syntax)
        {
            ManagementObjectSearcher mos = new("root\\CIMV2", "SELECT * FROM " + hwclass);
            foreach (ManagementObject mj in mos.Get())
            {
                if (Convert.ToString(mj[syntax]) != "")
                    return Convert.ToString(mj[syntax]);
            }

            return "";
        }
    }
}