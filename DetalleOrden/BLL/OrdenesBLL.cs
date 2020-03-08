using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DetalleOrden.DAL;
using DetalleOrden.Entidades;
using System.Linq;
using System.Linq.Expressions;

namespace DetalleOrden.BLL
{
    public class OrdenesBLL
    {
        public static bool Guardar(Ordenes orden)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Ordenes.Add(orden) != null)
                    paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Ordenes orden)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Database.ExecuteSqlRaw($"Delete FROM Ordenes Where ClienteId={orden.ClienteId}");
                foreach (var item in orden.OrdenDetalle)
                {
                    db.Entry(item).State = EntityState.Added;
                }
                db.Entry(orden).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var Eliminar = OrdenesBLL.Buscar(id);
                db.Entry(Eliminar).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Ordenes Buscar(int id)
        {
            Ordenes orden = new Ordenes();
            Contexto db = new Contexto();

            try
            {
                orden = db.Ordenes.Include(x => x.OrdenDetalle)
                     .Where(x => x.ClienteId == id)
                     .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return orden;
        }

        public static List<Ordenes> GetList(Expression<Func<Ordenes, bool>> orden)
        {
            List<Ordenes> Lista = new List<Ordenes>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Ordenes.Where(orden).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;
        }
    }
}
