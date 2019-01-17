import User from "./User";
import {OrderItem} from "./OrderItem";
import {OrderStatus} from "./OrderStatus";

export class Order{
    orderId : number;
    userId : string;
    date : string;
    statusId : number;
    street : string;
    postalCode : string;
    city : string;
    orderItems : OrderItem[];
    user : User;
    orderStatus : OrderStatus;
}