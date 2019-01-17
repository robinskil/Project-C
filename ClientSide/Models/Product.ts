import {Publisher} from "./Publisher";
import {GameType} from "./GameType";
import {PGRating} from "./PGRating";
import {ProductFeature} from "./ProductFeature";
import {ProductVideo} from "./ProductVideo";
import {Image} from "./Image";
import {Inventory} from "./Inventory";

export class Product{
    productId : number;
    name : string;
    releaseDate: Date;
    description: string;
    publisherId: number;
    gameTypeId : number;
    pgRatingId : number;
    publisher: Publisher;
    gameType: GameType;
    pgRating: PGRating;
    inventories : Inventory[];
    productFeatures : ProductFeature[];
    productVideos : ProductVideo[];
    productImages : Image[];
}
