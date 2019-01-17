import {GamePlatform} from "./GamePlatform";
import {Product} from "./Product";


export class Inventory {
    inventoryId:number;
    stock:number;
    price:number;
    productId:number;
    platformId:number;
    gamePlatform:GamePlatform;
    product : Product;
}