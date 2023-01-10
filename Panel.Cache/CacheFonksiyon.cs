
using Panel.Bll;
using Panel.Cache;
using Panel.Dal;
using Panel.Dal.Concrete.EntityFramework.Repository;
using Panel.Entities.Models;
using Panel.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panel.Cache
{
    public class CacheFonksiyon
    {
        DefaultCacheProvider provider = new DefaultCacheProvider();
        public void CacheTemizle()
        {
            provider.Remove(Enums.CacheKey.Roller.ToString());

        }




        public void CacheOlustur()
        {
            DefaultCacheProvider provider = new DefaultCacheProvider();


            #region Iller
            object ilCache = null;
            try
            {

                IGenericService<il> genericilservice = new GenericManager<il>(new EfGenericRepository<il>());
                var il = genericilservice.GetAll();

                if (il != null)
                    ilCache = il;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache'  Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.Iller.ToString(), ilCache);
            #endregion



            #region Ilce
            object ilceCache = null;
            try
            {
                IGenericService<ilce> genericService1 = new GenericManager<ilce>(new EfGenericRepository<ilce>());

                var ilce = genericService1.GetAll();

                if (ilce != null)
                    ilceCache = ilce;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.Ilceler.ToString(), ilceCache);
            #endregion


            #region RoleCacheOlustur

            object RoleCache = null;
            try
            {
                IGenericService<Role> rolegenericservice = new GenericManager<Role>(new EfGenericRepository<Role>());

                var roles = rolegenericservice.GetAll();

                if (roles != null)
                    RoleCache = roles;
                else
                    throw new Exception("Kategori Cache' Doldurulamadı.");
            }
            catch (Exception error)
            {
                Trace.WriteLine("Kategori Cache' Doldurulma Sırasında Hata Oluştu.");
                throw new Exception("Kategori Cache' Doldurulamadı.", error);
            }

            provider.Set(Enums.CacheKey.Roller.ToString(), RoleCache);
            #endregion





        }

        public object KategoriGetir()
        {
            object value = null;
            try
            {
                var kategori = (List<Tanim>)(provider.Get(Enums.CacheKey.Kategoriler.ToString()));

                if (kategori != null)
                    value = kategori;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }

        public object RolGetir()
        {
            object value = null;
            try
            {
                var rols = (List<Role>)(provider.Get(Enums.CacheKey.Roller.ToString()));

                if (rols != null)
                    value = rols;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }

        public object RolIDGetir(string RoleString = "")
        {
            object value = null;
            try
            {
                var rolename = (List<Role>)(provider.Get(Enums.CacheKey.Roller.ToString()));
                var roleID = rolename.Where(x => x.RoleName == RoleString);
                if (roleID != null)

                    value = roleID.ToList()[0].RoleReferenceID;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }








        public object ilGetir()
        {
            object value = null;
            try
            {
                var il = (List<il>)(provider.Get(Enums.CacheKey.Iller.ToString()));

                if (il != null)
                    value = il;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }

        public object ilceGetir()
        {
            object value = null;
            try
            {
                var ilce = (List<ilce>)(provider.Get(Enums.CacheKey.Ilceler.ToString()));

                if (ilce != null)
                    value = ilce;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);



            }

            return value;
        }
        public object KategoriGrupGetir()
        {
            object value = null;
            try
            {
                var kategorigrup = (List<TanimGrup>)(provider.Get(Enums.CacheKey.KategoriGrup.ToString()));


                if (kategorigrup != null)
                    value = kategorigrup;
            }
            catch (Exception error)
            {

                value = null;
                Trace.WriteLine("Kategori Cache'ten Okunamadı." + error.Message);
                throw new Exception("Kategori Cache'ten Okunamadı.", error);

            }

            return value;
        }












        public object CacheTanimGetir(string key)
        {
            object value = null;
            try
            {
                var tanim = (List<Tanim>)(provider.Get(key));

                if (tanim != null)
                    value = tanim;
            }
            catch (Exception error)
            {
                value = null;
                Trace.WriteLine(key + " Cache'ten Okunamadı." + error.Message);
                throw new Exception(key + " Cache'ten Okunamadı.", error);
            }

            return value;
        }
    }
}
