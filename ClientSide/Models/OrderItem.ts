
import {Order} from "./Order";
import {Inventory} from "./Inventory";

export class OrderItem{
    orderId : number;
    inventoryId : number;
    amount : number;
    priceBought : number;
    inventory : Inventory;
    order : Order;
}