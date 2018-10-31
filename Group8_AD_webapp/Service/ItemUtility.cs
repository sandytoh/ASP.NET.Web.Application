using Group8_AD_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Utility
{
    public static class ItemUtility
    {
        public static List<ItemVM> Convert_Item_To_ItemVM(List<Item> items)
        {

            List<ItemVM> itemlistvm = new List<ItemVM>();
            foreach (Item item in items)
            {
                ItemVM itemVM = new ItemVM();
                itemVM.ItemCode = item.ItemCode;
                itemVM.Cat = item.Cat;
                itemVM.Desc = item.Desc;
                itemVM.Location = item.Location;
                itemVM.UOM = item.UOM;
                itemVM.IsActive = item.IsActive;
                itemVM.Balance = item.Balance;
                itemVM.ReorderLevel = item.ReorderLevel;
                itemVM.ReorderQty = item.ReorderQty;
                itemVM.TempQtyDisb = item.TempQtyDisb;
                itemVM.TempQtyCheck = item.TempQtyCheck;
                itemVM.SuppCode1 = item.SuppCode1;
                itemVM.SuppCode2 = item.SuppCode2;
                itemVM.SuppCode3 = item.SuppCode3;
                itemVM.Price1 = item.Price1 ?? default(double);
                itemVM.Price2 = item.Price2 ?? default(double);
                itemVM.Price3 = item.Price3 ?? default(double);

                itemlistvm.Add(itemVM);
            }

            return itemlistvm;
        }


        public static List<Item> Convert_ItemVM_To_Item(List<ItemVM> itemsVM)
        {

            List<Item> itemlist = new List<Item>();
            foreach (ItemVM item in itemsVM)
            {
                Item i = new Item();
                i.ItemCode = item.ItemCode;
                i.Cat = item.Cat;
                i.Desc = item.Desc;
                i.Location = item.Location;
                i.UOM = item.UOM;
                i.IsActive = item.IsActive;
                i.Balance = item.Balance;
                i.ReorderLevel = item.ReorderLevel;
                i.ReorderQty = item.ReorderQty;
                i.TempQtyDisb = item.TempQtyDisb;
                i.TempQtyCheck = item.TempQtyCheck;
                i.SuppCode1 = item.SuppCode1;
                i.SuppCode2 = item.SuppCode2;
                i.SuppCode3 = item.SuppCode3;
                i.Price1 = item.Price1;
                i.Price2 = item.Price2;
                i.Price3 = item.Price3;

                itemlist.Add(i);
            }

            return itemlist;
        }


        public static ItemVM Convert_ItemObj_To_ItemVMObj(Item item)
        {
                ItemVM itemVM = new ItemVM();
                itemVM.ItemCode = item.ItemCode;
                itemVM.Cat = item.Cat;
                itemVM.Desc = item.Desc;
                itemVM.Location = item.Location;
                itemVM.UOM = item.UOM;
                itemVM.IsActive = item.IsActive;
                itemVM.Balance = item.Balance;
                itemVM.ReorderLevel = item.ReorderLevel;
                itemVM.ReorderQty = item.ReorderQty;
                itemVM.TempQtyDisb = item.TempQtyDisb;
                itemVM.TempQtyCheck = item.TempQtyCheck;
                itemVM.SuppCode1 = item.SuppCode1;
                itemVM.SuppCode2 = item.SuppCode2;
                itemVM.SuppCode3 = item.SuppCode3;
                itemVM.Price1 = item.Price1 ?? default(double);
                itemVM.Price2 = item.Price2 ?? default(double);
                itemVM.Price3 = item.Price3 ?? default(double);
            
            return itemVM;            
        }


        public static Item Convert_ItemVMObj_To_ItemObj(ItemVM item)
        {
                Item i = new Item();
                i.ItemCode = item.ItemCode;
                i.Cat = item.Cat;
                i.Desc = item.Desc;
                i.Location = item.Location;
                i.UOM = item.UOM;
                i.IsActive = item.IsActive;
                i.Balance = item.Balance;
                i.ReorderLevel = item.ReorderLevel;
                i.ReorderQty = item.ReorderQty;
                i.TempQtyDisb = item.TempQtyDisb;
                i.TempQtyCheck = item.TempQtyCheck;
                i.SuppCode1 = item.SuppCode1;
                i.SuppCode2 = item.SuppCode2;
                i.SuppCode3 = item.SuppCode3;
                i.Price1 = item.Price1;
                i.Price2 = item.Price2;
                i.Price3 = item.Price3;

            return i;
        }
    }
}