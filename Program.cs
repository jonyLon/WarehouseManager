using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Data.SqlTypes;
using System.Data;
using System;

namespace WarehouseManager
{

    class Product
    {

        public Product() { }
        public Product(int id, string name, int type, int supplier, int quantity, double cost, DateTime supplyDate) {
            Id = id;
            ProductName = name;
            TypeId = type;
            SupplierId = supplier;
            Quantity = quantity;
            Cost = cost;
            SupplyDate = supplyDate;
        }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int TypeId { get; set; }
        public int SupplierId { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public DateTime SupplyDate { get; set; }

        public override string ToString()
        {
            return $"Id: {Id, -5} Name: {ProductName,-10} TypeId: {TypeId,-5} SupplierId: {SupplierId,-5} Quantity: {Quantity,-5} Cost: {Cost,-5} SupplyDate: {SupplyDate,-5}";
        }

    }

    class ProductType
    {
        public ProductType() { }
        public ProductType(int id, string typeName)
        {
            Id = id;
            TypeName = typeName;
        }

        public int Id { get; set; }
        public string TypeName {  get; set; }

        public override string ToString()
        {
            return $"Id: {Id,-5} Name: {TypeName,-10}";
        }
    }

    class Supplier
    {

        public Supplier() { }
        public Supplier(int id, string supplierName)
        {
            Id= id;
            SupplierName = supplierName;
        }
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public override string ToString()
        {
            return $"Id: {Id,-5} Name: {SupplierName,-10}";
        }
    }




    class DataManager
    {

        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;
        public DataManager(string con) {
            connection = new SqlConnection(con);
            connection.Open();
        }




        public int CreateProduct(Product prod)
        {
            string query = "insert into Products values (@id, @name, @type, @supplier, @quantity, @cost, @supply_date)";
            command = new SqlCommand(query, connection);
            SetProdParams(prod);
            return command.ExecuteNonQuery();
        }

        private void SetProdParams(Product prod)
        {
            command.Parameters.Add("@id", SqlDbType.Int).Value = prod.Id;
            command.Parameters.Add("@name", SqlDbType.NVarChar).Value = prod.ProductName;
            command.Parameters.Add("@type", SqlDbType.Int).Value = prod.TypeId;
            command.Parameters.Add("@supplier", SqlDbType.Int).Value = prod.SupplierId;
            command.Parameters.Add("@quantity", SqlDbType.Int).Value = prod.Quantity;
            command.Parameters.Add("@cost", SqlDbType.Int).Value = prod.Cost;
            command.Parameters.Add("@supply_date", SqlDbType.Date).Value = prod.SupplyDate;
        }


        public int CreateType(ProductType type)
        {
            string query = "insert into ProductTypes values (@id, @type)";
            command = new SqlCommand(query, connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = type.Id;
            command.Parameters.Add("@type", SqlDbType.NVarChar).Value = type.TypeName;
            return command.ExecuteNonQuery();
        }
        public int CreateSupplier(Supplier supplier)
        {
            string query = "insert into Suppliers values (@id, @supplier)";
            command = new SqlCommand(query, connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = supplier.Id;
            command.Parameters.Add("@supplier", SqlDbType.NVarChar).Value = supplier.SupplierName;
            return command.ExecuteNonQuery();
        }

        public List<Product> ReadProducts()
        {
            string query = "select * from Products";
            command = new SqlCommand(query, connection);
            reader = command.ExecuteReader();
            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                products.Add(new Product()
                {
                    Id = (int)reader[0],
                    ProductName = (string)reader[1],
                    TypeId = (int)reader[2],
                    SupplierId = (int)reader[3],
                    Quantity = (int)reader[4],
                    Cost = (double)(decimal)reader[5],
                    SupplyDate = (DateTime)reader[6]
                }); 
            }

            reader.Close();
            return products;
        }

        public Product ReadOneProduct(int id)
        {
            string query = "select * from Products where ProductId = @id";
            command = new SqlCommand(query, connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            reader = command.ExecuteReader();
            reader.Read();
            Product prod = new Product()
                {
                    Id = (int)reader[0],
                    ProductName = (string)reader[1],
                    TypeId = (int)reader[2],
                    SupplierId = (int)reader[3],
                    Quantity = (int)reader[4],
                    Cost = (double)(decimal)reader[5],
                    SupplyDate = (DateTime)reader[6]
                };
            reader.Close();
            return prod;
        }

        public List<ProductType> ReadProductTypes()
        {
            string query = "select * from ProductTypes";
            command = new SqlCommand(query, connection);
            reader = command.ExecuteReader();
            List<ProductType> types = new List<ProductType>();
            while (reader.Read())
            {
                types.Add(new ProductType()
                {
                    Id = (int)reader[0],
                    TypeName = (string)reader[1],
                });
            }

            reader.Close();
            return types;
        }
        public ProductType ReadOneProductType(int id)
        {
            string query = "select * from ProductTypes where TypeID = @id";
            command = new SqlCommand(query, connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            reader = command.ExecuteReader();
            reader.Read();
            var type = new ProductType()
            {
                Id = (int)reader[0],
                TypeName = (string)reader[1],
            };


            reader.Close();
            return type;
        }
        public List<Supplier> ReadSuppliers()
        {
            string query = "select * from Suppliers";
            command = new SqlCommand(query, connection);
            reader = command.ExecuteReader();
            List<Supplier> supps = new List<Supplier>();
            while (reader.Read())
            {
                supps.Add(new Supplier()
                {
                    Id = (int)reader[0],
                    SupplierName = (string)reader[1],
                });
            }

            reader.Close();
            return supps;
        }
        public Supplier ReadOneSupplier(int id)
        {
            string query = "select * from Suppliers where SupplierID = @id";
            command = new SqlCommand(query, connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            reader = command.ExecuteReader();
            reader.Read();
            var supp = new Supplier()
            {
                Id = (int)reader[0],
                SupplierName = (string)reader[1],
            };

            reader.Close();
            return supp;
        }

        public int UpdateProduct(Product prod)
        {
            string query = "update Products set\n\t" +
                "ProductID = @id,\n\tProductName = @name,\n\tTypeID = @type,\n\tSupplierID = @supplier,\n\tQuantity = @quantity,\n\tCost = @cost,\n\tSupplyDate = @supply_date\nwhere ProductID = @id";
            command = new SqlCommand(query, connection);
            SetProdParams(prod);
            return command.ExecuteNonQuery();
        }

        public int UpdateProductType(ProductType type)
        {
            string query = "update ProductTypes set\n\t" +
                "TypeID = @id,\n\tTypeName = @type\nwhere TypeID = @id";
            command = new SqlCommand(query, connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = type.Id;
            command.Parameters.Add("@type", SqlDbType.NVarChar).Value = type.TypeName;
            return command.ExecuteNonQuery();
        }

        public int UpdateSuppliers(Supplier supplier)
        {
            string query = "update Suppliers set\n\t" +
                "SupplierID = @id,\n\tSupplierName = @supplier\nwhere SupplierID = @id";
            command = new SqlCommand(query, connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = supplier.Id;
            command.Parameters.Add("@supplier", SqlDbType.NVarChar).Value = supplier.SupplierName;
            return command.ExecuteNonQuery();
        }

        public int DeleteProduct(int id)
        {
            string query = "delete Products\nwhere ProductID = @id";
            command = new SqlCommand(query, connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            return command.ExecuteNonQuery();
        }

        public int DeleteProductType(int id)
        {
            string query = "delete ProductTypes\nwhere TypeID = @id";
            command = new SqlCommand(query, connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            return command.ExecuteNonQuery();
        }

        public int DeleteSuppliers(int id)
        {
            string query = "delete Suppliers\nwhere SupplierID = @id";
            command = new SqlCommand(query, connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            return command.ExecuteNonQuery();
        }




        ~DataManager()
        {
            connection.Close();
        }
    }





    internal class Program
    {
        static void Main(string[] args)
        {
            string conn = ConfigurationManager.ConnectionStrings["Warehouse"].ConnectionString;
            var dm = new DataManager(conn);

            //var list = dm.ReadProducts();
            //list.ForEach(p => Console.WriteLine(p.ToString()));
            //var list_t = dm.ReadProductTypes();
            //list_t.ForEach(p => Console.WriteLine(p.ToString()));
            //var list_s = dm.ReadSuppliers();
            //list_s.ForEach(p => Console.WriteLine(p.ToString()));

            //Product p = new Product(4, "Desk", 4, 4, 40, 200.00, new DateTime(2024, 01, 20)) { };
            //ProductType type = new ProductType(4, "Furniture");
            //Supplier supplier = new Supplier(4, "Furnishings Pro");
            //Console.WriteLine(dm.CreateType(type));
            //Console.WriteLine(dm.CreateSupplier(supplier));
            //Console.WriteLine(dm.CreateProduct(p));

            //var pp = dm.ReadOneProduct(4);
            //Console.WriteLine(pp);
            //Console.WriteLine(dm.UpdateProduct(pp));
            //pp = dm.ReadOneProduct(4);
            //Console.WriteLine(pp);

            //var pt = dm.ReadOneProductType(4);
            //pt.TypeName += "_up";
            //Console.WriteLine(dm.UpdateProductType(pt));
            //pt = dm.ReadOneProductType(4);
            //Console.WriteLine(pt);
            //var sp = dm.ReadOneSupplier(4);
            //sp.SupplierName += "_up";
            //Console.WriteLine(dm.UpdateSuppliers(sp));
            //sp = dm.ReadOneSupplier(4);
            //Console.WriteLine(sp);



            dm.DeleteProduct(4);
            dm.DeleteProductType(4);
            dm.DeleteSuppliers(4);

            var list = dm.ReadProducts();
            list.ForEach(p => Console.WriteLine(p.ToString()));
            var list_t = dm.ReadProductTypes();
            list_t.ForEach(p => Console.WriteLine(p.ToString()));
            var list_s = dm.ReadSuppliers();
            list_s.ForEach(p => Console.WriteLine(p.ToString()));

        }
    }
}