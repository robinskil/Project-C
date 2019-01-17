import { Inventory } from "../Models/Inventory";

//export default class Product {
//    ProductId: number;
//    Name: string;
//    ReleaseDate: Date;
//    Description: string;
//    PublisherId: number;
//    GameTypeId: number;
//    PGRatingId: number;
//    Publisher: Publisher;
//    GameType: GameType;
//    PgRating: PGRating;
//    Inventories: Inventory[];
//    ProductFeatures: ProductFeature[];
//    ProductVideos: ProductVideo[];
//    ProductImages: Image[];
//}
//class Publisher {
//    PublisherId: number;
//    PublisherName: string;
//}
//class PGRating {
//    PGRatingId: number;
//    Rating: number;
//}
//class GameType {
//    GameTypeId: number;
//    GenreName: string;
//}
//class GamePlatform {
//    PlatformId: number;
//    PlatformName: string;
//}
//export class Inventory {
//    InventoryId: number;
//    Stock: number;
//    Price: number;
//    ProductId: number;
//    PlatformId: number;
//    GamePlatform: GamePlatform;
//}
//class ProductVideo {
//    ProductVideoId: number;
//    URL: string;
//}
//class ProductFeature {
//    FeatureId: number;
//    Feature: string;
//}
//export class Image {
//    ImageId: number;
//    URL: string;
//}
/**
 * Returns a product specified by the url
 * @param productId
 * @constructor
 */
//export async function JsonToProduct(productId: number): Promise<Product> {
//    //let p:Product = new Product();
//    let url = window.location.origin + "/products/GetProduct?ProductId=" + productId;
//    console.log(url);
//    let response = await fetch(url, { credentials: "same-origin" });
//    var answer = await response.text();
//    console.log(answer);
//    let JsonObject: Product = JSON.parse(answer);
//    console.log(JsonObject.ProductId);
//    return JsonObject;
//}
/**
 * Add cookie for product in shopping cart
 * @param productId
 * @param platformId
 * @constructor
 */
export async function AddToCart(productId: number, platformId: number, inventories: Inventory[]) {
    for (let x = 0; inventories.length; x++) {
        if (inventories[x].platformId == platformId) {
            if(inventories[x].stock < 1){
                alert("This product is not in stock , therefore u cannot add this to your shopping cart");
                return;
            }
            var inventoryId = inventories[x].inventoryId;
            break;
        }
    }
    let url = window.location.origin + "/ShoppingCart/AddToCart?inventoryId=" + inventoryId;
    await fetch(url, {
        credentials: "same-origin"
    }).then(response => {
        if (response.ok) {
            alert("Added to shopping cart");
            console.log("Cookie added succesfully");
        }
        else {
            console.log("Smth went wrong....");
        }
    });
}

export async function AddToWishList(productId: number, platformId: number, inventories: Inventory[]) {
    for (let x = 0; inventories.length; x++) {
        if (inventories[x].platformId == platformId) {
            var inventoryId = inventories[x].inventoryId;
            break;
        }
    }
    let url = window.location.origin + "/WishList/AddToWishList?InventoryId=" + inventoryId;
    await fetch(url, { credentials: "same-origin" }).then(response => {
        if (response.ok) {
            alert("Added to wishlist");
            console.log("wishlist item added succesfully");
        }
        else {
            console.log("Smth went wrong....");
        }
    });
}



