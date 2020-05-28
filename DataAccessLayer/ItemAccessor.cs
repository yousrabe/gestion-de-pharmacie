using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObject;
using System.Data.SqlClient;
using System.Data;
namespace DataAccessLayer
{
  public  class ItemAccessor
    {

        public static List<Item> RetreiveItemsByType(string type)
        {
            List<Item> items = new List<Item>();


            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_retrieve_items_by_type";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@TypeID", SqlDbType.NVarChar, 50);

            // values
            cmd.Parameters["@TypeID"].Value = type;

            try
            {
                // open the connection
                conn.Open();

                // process cmd
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        items.Add(new Item()
                        {
                            ItemId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Type = reader.GetString(3),
                            Strength = reader.GetString(4),
                            Picture = reader.GetString(5),
                            Description = reader.GetString(6),
                            OnStock = reader.GetBoolean(7)
                        });

                    }
                }
                reader.Close(); // done with this reader
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return items;
        }

        public static Item RetreiveItemById(int ItemId)
        {
            Item item = new Item();


            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_retrieve_items_by_id";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@ItemID", SqlDbType.Int, 32);

            // values
            cmd.Parameters["@ItemID"].Value = ItemId;

            try
            {
                // open the connection
                conn.Open();

                // process cmd
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        item = new Item
                        {
                            ItemId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Type = reader.GetString(3),
                            Strength = reader.GetString(4),
                            Picture = reader.GetString(5),
                            Description = reader.GetString(6),
                            OnStock = reader.GetBoolean(7)
                        };

                    }
                }
                reader.Close(); // done with this reader
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return item;
        }

        public static List<Item> RetreiveItemsById(int ItemId)
        {
            List<Item> items = new List<Item>();


            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_retrieve_items_by_id";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@ItemID", SqlDbType.Int, 32);

            // values
            cmd.Parameters["@ItemID"].Value = ItemId;

            try
            {
                // open the connection
                conn.Open();

                // process cmd
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        items.Add(new Item()
                        {
                            ItemId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Type = reader.GetString(3),
                            Strength = reader.GetString(4),
                            Picture = reader.GetString(5),
                            Description = reader.GetString(6),
                            OnStock = reader.GetBoolean(7)
                        });

                    }
                }
                reader.Close(); // done with this reader
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return items;
        }

        public static List<Item> RetreiveItemsByName(string name)
        {
            List<Item> items = new List<Item>();


            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_retrieve_items_by_name";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50);

            // values
            cmd.Parameters["@Name"].Value = name;

            try
            {
                // open the connection
                conn.Open();

                // process cmd
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        items.Add(new Item()
                        {
                            ItemId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Type = reader.GetString(3),
                            Strength = reader.GetString(4),
                            Picture = reader.GetString(5),
                            Description = reader.GetString(6),
                            OnStock = reader.GetBoolean(7)
                        });

                    }
                }
                reader.Close(); // done with this reader
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return items;
        }

        public static List<Item> RetreiveAllItems()
        {
        List<Item> items = new List<Item>();


        // get a connection
        var conn = DBConnection.GetDbConnection();

        // command text
        string cmdText = @"sp_retrieve_all_items";

        // command objects
        var cmd = new SqlCommand(cmdText, conn);

        // set the command type
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            // open the connection
            conn.Open();

            // process cmd
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                        items.Add(new Item()
                    {
                        ItemId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Type = reader.GetString(3),
                        Strength = reader.GetString(4),
                        Picture = reader.GetString(5),
                        Description = reader.GetString(6),
                        OnStock = reader.GetBoolean(7)
                    });

                }
            }
            reader.Close(); // done with this reader
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            conn.Close();
        }
        return items;
    }


        public static List<string> SelectAllItemTypes()
        {
            var itemTypes = new List<string>();

            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_retreive_all_itemtypeid";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                // open the connection
                conn.Open();

                // process cmd2
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    { itemTypes.Add(reader.GetString(0)); }

                }
                reader.Close(); // done with this reader
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return itemTypes;
        }


        public static int UpdateItem(Item oldItem, Item newItem)
        {
            int rows1 = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_item_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemID", oldItem.ItemId);

            cmd.Parameters.AddWithValue("@Name", newItem.Name);
            cmd.Parameters.AddWithValue("@Description", newItem.Description);
            cmd.Parameters.AddWithValue("@Price", newItem.Price);
            cmd.Parameters.AddWithValue("@Type", newItem.Type);
            cmd.Parameters.AddWithValue("@Strength", newItem.Strength);
            cmd.Parameters.AddWithValue("@Exist", newItem.OnStock);
            cmd.Parameters.AddWithValue("@Picture", newItem.Picture);
            cmd.Parameters.AddWithValue("@OldName", oldItem.Name);
            cmd.Parameters.AddWithValue("@OldPrice", oldItem.Price);
            cmd.Parameters.AddWithValue("@OldType", oldItem.Type);
            cmd.Parameters.AddWithValue("@OldStrength", oldItem.Strength);
            cmd.Parameters.AddWithValue("@OldDescription", oldItem.Description);
            cmd.Parameters.AddWithValue("@OldExist", oldItem.OnStock);
            cmd.Parameters.AddWithValue("@OldPicture", oldItem.Picture);

            try
            {
                conn.Open();
                rows1 = cmd.ExecuteNonQuery();
             
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows1;
        }

        public static int InsertItem(Item newItem)
        {
            int rows1 = 0;


            var conn = DBConnection.GetDbConnection();
            var cmdText1 = @"sp_insert_item";
            var cmd1 = new SqlCommand(cmdText1, conn);
            cmd1.CommandType = CommandType.StoredProcedure;

            cmd1.Parameters.AddWithValue("@ItemID", newItem.ItemId);
            cmd1.Parameters.AddWithValue("@Name", newItem.Name);
            cmd1.Parameters.AddWithValue("@Price", newItem.Price);
            cmd1.Parameters.AddWithValue("@Type", newItem.Type);
            cmd1.Parameters.AddWithValue("@Strength", newItem.Strength);
            cmd1.Parameters.AddWithValue("@Picture", newItem.Picture);
            cmd1.Parameters.AddWithValue("@Description", newItem.Description);
            cmd1.Parameters.AddWithValue("@Exist", newItem.OnStock);

            try
            {
                conn.Open();
                rows1 = cmd1.ExecuteNonQuery();
             
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows1;
        }


        public static int DeleteItemById(int itemId)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_items_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemID", itemId);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public static int DeactivateItemByID(int ID)
        {
            int result = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_deactivate_items_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ItemID", ID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public static int ReactivateItemByID(int ID)
        {
            int result = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_activate_items_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ItemID", ID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
