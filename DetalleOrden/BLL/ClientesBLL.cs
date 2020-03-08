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
    public class ClientesBLL
    {
        public static bool Guardar(Clientes cliente)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Clientes.Add(cliente) != null)
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

        public static bool Modificar(Clientes cliente)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Database.ExecuteSqlRaw($"Delete FROM Clientes Where ClienteId={cliente.ClienteId}");
                foreach (var item in cliente.Orden)
                {
                    db.Entry(item).State = EntityState.Added;
                }
                db.Entry(cliente).State = EntityState.Modified;
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
                var Eliminar = ClientesBLL.Buscar(id);
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

        public static Clientes Buscar(int id)
        {
            Clientes cliente = new Clientes();
            Contexto db = new Contexto();

            try
            {
                cliente = db.Clientes.Include(x => x.Orden)
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
            return cliente;
        }

        public static List<Clientes> GetList(Expression<Func<Clientes, bool>> cliente)
        {
            List<Clientes> Lista = new List<Clientes>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Clientes.Where(cliente).ToList();
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
