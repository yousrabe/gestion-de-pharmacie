using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObject;

namespace LogicLayer
{
   public class ItemManager
    {
        public List<Item> RetrieveItems()
        {
            List<Item> items = null;

            try
            {
               items = ItemAccessor.RetreiveAllItems();
            }
            catch (Exception)
            {
                throw ;
      
            }

            return items;
        }
        public List<Item> RetrieveItemsByType(string type = "All")
        {
            List<Item> items = null;

            try
            {
                items = ItemAccessor.RetreiveItemsByType(type);
            }
            catch (Exception)
            {
                throw;
            }

            return items;
        }
        public Item RetrieveItemById(int ID)
        {
            Item item = null;

            try
            {
                item = ItemAccessor.RetreiveItemById(ID);
            }
            catch (Exception)
            {
                throw;
            }

            return item;
        }
        public List<Item> RetrieveItemsById(int ID)
        {
            List<Item> items = null;

            try
            {
                items = ItemAccessor.RetreiveItemsById(ID);
            }
            catch (Exception)
            {
                throw;
            }

            return items;
        }
        public List<Item> RetrieveItemsByName(string name)
        {
            List<Item> items = null;

            try
            {
                items = ItemAccessor.RetreiveItemsByName(name);
            }
            catch (Exception)
            {
                throw;
            }

            return items;
        }

        public List<string> RetrieveAllItemTypes()
        {
            List<string> ItemTypes = null;
            try
            {
                ItemTypes = ItemAccessor.SelectAllItemTypes();
            }
            catch (Exception)
            {
                throw;
            }
            return ItemTypes;
        }


        public bool EditItem(Item oldItem, Item newItem)
        {
            bool result = false;

            try
            {
                result = (1 == ItemAccessor.UpdateItem(oldItem, newItem));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public bool AddItem(Item newItem)
        {
            bool result = false;

            try
            {
                result = (1 == ItemAccessor.InsertItem(newItem));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        
        public bool Delete(int ItemId)
        {
            bool result = false;

            try
            {
                result = (1 == ItemAccessor.DeleteItemById(ItemId));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        public bool DeactivateItemByID(int ID)
        {
            try
            {
                return (1 == ItemAccessor.DeactivateItemByID(ID));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ReactivateItemByID(int ID)
        {
           try
            {
                return (1 == ItemAccessor.ReactivateItemByID(ID));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
