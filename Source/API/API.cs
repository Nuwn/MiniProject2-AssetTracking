using DataModels;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public static class Action
    {
        public static void Save() => AppDBContext.Get.SaveChanges();
    }

    public static class Assets
    {
        public static List<Asset> GetAll => AppDBContext.Get.Assets.Include(x => x.Device).Include(x => x.Office).ToList();
        public static Asset? GetSingle(int id) => AppDBContext.Get.Assets.Include(x => x.Device).Include(x => x.Office).FirstOrDefault(x => x.Id == id);
        public static void Delete(Asset asset) => AppDBContext.Get.Assets.Remove(asset);
        public static void Add(Asset asset) => AppDBContext.Get.Assets.Add(asset);
        public static void Update(Asset update) => AppDBContext.Get.Assets.Update(update);
        public static int Count => AppDBContext.Get.Assets.Count();
        public static bool Has(int id) => AppDBContext.Get.Assets.Any(x => x.Id == id);
    }

    public static class Offices
    {
        public static List<Office> GetAll => AppDBContext.Get.Offices.ToList();
        public static Office? GetSingle(int id) => AppDBContext.Get.Offices.FirstOrDefault(x => x.Id.Equals(id));
        public static void Delete(Office office) => AppDBContext.Get.Offices.Remove(office);
        public static void Add(Office office) => AppDBContext.Get.Offices.Add(office);
        public static void Update(Office office) => AppDBContext.Get.Offices.Update(office);
        public static bool Has(int id) => AppDBContext.Get.Offices.Any(x => x.Id == id);
        public static int Count => AppDBContext.Get.Offices.Count();
    }

    public static class Devices
    {
        public static void Delete(Device device) => AppDBContext.Get.Devices.Remove(device);
    }
}
