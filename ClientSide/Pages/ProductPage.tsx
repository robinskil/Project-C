import * as React from "react";
import * as ReactDOM from "react-dom";
import { AddToCart, AddToWishList } from "../BackEndClientSide/backend";
import { Product } from "../Models/Product";
import { Inventory } from "../Models/Inventory";
import { Image } from "../Models/Image";

/*finished imports here , under here all page components imported by app.tsx*/
var activeImage: boolean = true;
var activeController: boolean = true;
var activePlatformSelect: boolean = true;
var slideToNumber: number = 0;
var productIdToGet: number = 1;
var platformIdToGet: number = 1;
var selectedPlatform: number = 1;
var userIsLoggedOn : boolean = false;

export class TitleManager extends React.Component<{ product: Product, platformId: number }, { platformId: number }>{
    constructor(props: any) {
        super(props);
        //productIdToGet = this.props.productId;
        //console.log(this.props.productId);
        platformIdToGet = this.props.platformId;
        selectedPlatform = platformIdToGet;
        this.state = { platformId: selectedPlatform };
        updateText = updateText.bind(this);
    }
    render() {
        return (
            <div>
                <h1>{this.props.product.name + " - " + GetPlatformName(this.props.product.inventories, this.state.platformId)}</h1>
                <ProductCarousel product={this.props.product} />
                <ProductInformation product={this.props.product} />
            </div>
        )
    }
}
class ProductInformation extends React.Component<{ product: Product }>{
    constructor(props: any) {
        super(props);
    }
    render() {
        return (
            <div className="row" style={{ paddingTop: "25px" }}>
                <div className="col-md-8">
                    <h3>Product description</h3>
                    <p>
                        {this.props.product.description}
                    </p>
                </div>
                <div className="col-md-auto">
                    <h3>
                        <small className="text-muted">Product features</small>
                    </h3>
                    <div style={{ paddingLeft: "15px" }}>
                        {this.props.product.productFeatures.map(productFeature => {
                            return (
                                <div className="row">
                                    <p>
                                        &#9679; {productFeature.feature}
                                    </p>
                                </div>
                            )
                        })}
                    </div>
                </div>
            </div>
        )
    }
}

class ProductCarousel extends React.Component<{ product: Product }>{
    constructor(props: any) {
        super(props);
    }
    render() {
        return (
            <div className="row">
                <div className="col-md-8">
                {this.props.product.productImages && this.props.product.productImages.length > 0 ? 
                    <div id="carouselitem" className="carousel slide carousel-fade" data-ride="carousel" data-interval="false">
                    <div className="carousel-inner" role="listbox">
                        {
                            this.props.product.productImages.map(image => {
                                return (
                                    <ProductImageComp image={image} />
                                )
                            })}
                    </div>
                    <div className="controlBlock">
                        <a className="carousel-control-prev" href="#carouselitem" role="button" data-slide="prev">
                            <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span className="sr-only">Previous</span>
                        </a>
                        <a className="carousel-control-next" href="#carouselitem" role="button" data-slide="next">
                            <span className="carousel-control-next-icon" aria-hidden="true"></span>
                            <span className="sr-only">Next</span>
                        </a>
                        <div>
                            <ol className="carousel-indicators">
                                {
                                    this.props.product.productImages.map(image => {
                                        return (<ProductImageController image={image} />)
                                    })
                                }
                            </ol>
                        </div>
                    </div>
                </div>
                : <img style={{height:"300px"}} src={"/images/no-image.jpg"}></img>
                }    
                </div>
                <div className="col-md-auto">
                    <div style={{ paddingLeft: '15px' }}>
                        <div className="row">
                            <h3>Select ur platform:</h3>
                        </div>
                        <div className="row">
                            <div className="btn-group btn-group-toggle" data-toggle="buttons" style={{ backgroundColor: "#29B6F6" }} id="toggleGroup">
                                <ButtonGroupOwn inventories={this.props.product.inventories} />
                            </div>
                        </div>
                        <PriceDisplay inventory={GetInventory(this.props.product.inventories)} />

                        <div className="row">
                            <div style={{ paddingRight: '10px' }}>
                                <button className="btn btn-outline-primary rounded-circle btnClick" onClick={() => AddToCart(this.props.product.productId, selectedPlatform, this.props.product.inventories)}>
                                    <i className="fa fa-shopping-cart "></i>
                                </button>
                            </div>
                            <p style={{ paddingTop: '5px' }}>Add to shopping cart</p>
                        </div>
                        {userIsLoggedOn ? (
                            <div className="row" style={{ paddingTop: "5px" }}>
                            <div style={{ paddingRight: '10px' }}>
                                <button className="btn btn-outline-primary rounded-circle btnClick" onClick={() => AddToWishList(this.props.product.productId, selectedPlatform, this.props.product.inventories)}>
                                    <i className="fa fa-list-ol "></i>
                                </button>
                            </div>
                            <p style={{ paddingTop: '5px' }}>Add to my wishlist</p>
                        </div>

                        ) :                             
                        <div className="row" style={{ paddingTop: "5px" }}>
                            <div style={{ paddingRight: '10px' }}>
                                <a className="btn btn-outline-primary rounded-circle btnClick" href="/WishList/AddToWishList">
                                    <i className="fa fa-list-ol "></i>
                                </a>
                            </div>
                            <p style={{ paddingTop: '5px' }}>Add to my wishlist</p>
                        </div>
                        }
                        
                    </div>
                </div>
            </div>
        )
    }
}
class PriceDisplay extends React.Component<{ inventory: Inventory }>{
    constructor(props: any) {
        super(props);

    }
    render() {
        return (
            <div>
            <div className="row" style={{ paddingTop: '15px' }}>
                <div className="col-auto; justify-content-start">
                    <p id="priceDesc" className="font-weight-bold" style={{ fontSize: '25px' }}>{'\u20AC'}{this.props.inventory.price}</p>
                </div>
                <div className="col-auto">
                    <div className="inputWithIcon">
                        <div id="stockDisplay">
                            <GetInnerDisplay gamePlatform={this.props.inventory.gamePlatform} inventoryId={this.props.inventory.inventoryId} platformId={this.props.inventory.platformId} price={this.props.inventory.price} productId={this.props.inventory.productId} stock={this.props.inventory.stock} product={null} />
                        </div>
                    </div>
                </div>
            </div>
                {this.props.inventory.stock > 0 ?  <div className="row" style={{ marginTop: "-5px" }}>
                            <p>Ordered before 18:00 today, receive it tomorrow</p>
                </div> : null}
            </div>
        )
    }

}
class ButtonGroupOwn extends React.Component<{ inventories: Inventory[] }>{
    constructor(props: any) {
        super(props);
    }
    render() {
        return (
            <div className="btn-group">
                {this.props.inventories.map(inventory => {
                    if (inventory.platformId == platformIdToGet) {
                        activePlatformSelect = false;
                        return (
                            <label className="btn btn-primary active" typeof="button" onClick={() => ButtonGroupOwn.SetPlatform(inventory.platformId)}>
                                <input name="options" type="radio" /> {inventory.gamePlatform.platformName}
                            </label>
                        )
                    }
                    else {
                        return (
                            <label className="btn btn-primary" typeof="button" onClick={() => ButtonGroupOwn.SetPlatform(inventory.platformId)}>
                                <input name="options" type="radio" /> {inventory.gamePlatform.platformName}
                            </label>
                        )
                    }
                })}
            </div>
        )
    }
    private static SetPlatform(id: number): void {
        selectedPlatform = id;
        console.log(selectedPlatform + "\t" + productIdToGet);
        updateText(id);
    }
}
class ProductImageComp extends React.Component<{ image: Image }>{
    constructor(props: any) {
        super(props);
    }
    render() {
        if (activeImage) {
            activeImage = false;
            return (
                <div className="carousel-item active">
                    <img className="rounded embed-responsive-item" src={this.props.image.url} alt="First slide" />
                </div>
            )
        } else {
            return (
                <div className="carousel-item">
                    <img className="rounded embed-responsive-item" src={this.props.image.url} alt="First slide" />
                </div>
            )
        }

    }
}
class ProductImageController extends React.Component<{ image: Image }>{
    constructor(props: any) {
        super(props);
    }
    render() {
        if (activeController) {
            activeController = false;
            return (
                <li data-target="#carouselitem" data-slide-to="0" className="active">
                    <img src={this.props.image.url} />
                </li>
            )
        } else {
            slideToNumber++;
            return (
                <li data-target="#carouselitem" data-slide-to={slideToNumber}>
                    <img src={this.props.image.url} />
                </li>
            )
        }
    }
}




//function definitions
function GetInnerDisplay(inventory: Inventory) {
    if (inventory.stock > 0) {
        return (
            <div>
                <p className="text-success" style={{ paddingRight: "20px", fontSize: '25px' }}>In Stock</p>
                < i className="fa fa-check" aria-hidden="true" style={{ fontSize: "25px", position: "absolute", top: "6px" }} />
            </div>
        )
    }
    else {
        return (
            <div>
                <p className="text-danger" style={{ paddingRight: "15px", fontSize: "25px" }}>Not In Stock</p>
                <i className="fa fa-close" aria-hidden="true" style={{ fontSize: "25px", position: "absolute", top: "8px" , color: "rgb(220, 53, 69)"}} />
            </div>
        )
    }
}




function GetPlatformName(inventories: Inventory[], platformId: number): string {
    for (let i = 0; i < inventories.length; i++) {
        if (selectedPlatform == inventories[i].platformId) {
            return inventories[i].gamePlatform.platformName;
        }
    }
    return "";
}
function GetInventory(inventories: Inventory[]): Inventory {
    for (let i = 0; i < inventories.length; i++) {
        if (selectedPlatform == inventories[i].platformId) {
            return inventories[i];
        }
    }
}

var updateText = function (platformId: number) {
    activeController = true;
    activeImage = true;
    slideToNumber = 0;
    this.setState({ platformId: platformId })
}

var urlParams = new URLSearchParams(window.location.search);
var productName = urlParams.get('name');
var platformName = urlParams.get('platform');
console.log(productName + "\t" + platformName);

if (productName && platformName) {
    let jsonString: string = document.currentScript.getAttribute("JSON");
    let loggedOn : boolean = document.currentScript.getAttribute("LoggedOn") === "LoggedOn";
    if(loggedOn){
        userIsLoggedOn = true;
    }
    let product: Product = JSON.parse(jsonString);
    console.log(product);
    let platformId;
    for (let x = 0; x < product.inventories.length; x++) {
        if (platformName == product.inventories[x].gamePlatform.platformName) {
            platformId = product.inventories[x].platformId;
        }
    }
    if (platformId) {
        ReactDOM.render(
            <TitleManager product={product} platformId={platformId} />
            ,
            document.getElementById("root")
        );
    }
}