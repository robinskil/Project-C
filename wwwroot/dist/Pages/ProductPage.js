"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const ReactDOM = require("react-dom");
const backend_1 = require("../BackEndClientSide/backend");
/*finished imports here , under here all page components imported by app.tsx*/
var activeImage = true;
var activeController = true;
var activePlatformSelect = true;
var slideToNumber = 0;
var productIdToGet = 1;
var platformIdToGet = 1;
var selectedPlatform = 1;
var userIsLoggedOn = false;
class TitleManager extends React.Component {
    constructor(props) {
        super(props);
        //productIdToGet = this.props.productId;
        //console.log(this.props.productId);
        platformIdToGet = this.props.platformId;
        selectedPlatform = platformIdToGet;
        this.state = { platformId: selectedPlatform };
        updateText = updateText.bind(this);
    }
    render() {
        return (React.createElement("div", null,
            React.createElement("h1", null, this.props.product.name + " - " + GetPlatformName(this.props.product.inventories, this.state.platformId)),
            React.createElement(ProductCarousel, { product: this.props.product }),
            React.createElement(ProductInformation, { product: this.props.product })));
    }
}
exports.TitleManager = TitleManager;
class ProductInformation extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (React.createElement("div", { className: "row", style: { paddingTop: "25px" } },
            React.createElement("div", { className: "col-md-8" },
                React.createElement("h3", null, "Product description"),
                React.createElement("p", null, this.props.product.description)),
            React.createElement("div", { className: "col-md-auto" },
                React.createElement("h3", null,
                    React.createElement("small", { className: "text-muted" }, "Product features")),
                React.createElement("div", { style: { paddingLeft: "15px" } }, this.props.product.productFeatures.map(productFeature => {
                    return (React.createElement("div", { className: "row" },
                        React.createElement("p", null,
                            "\u25CF ",
                            productFeature.feature)));
                })))));
    }
}
class ProductCarousel extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (React.createElement("div", { className: "row" },
            React.createElement("div", { className: "col-md-8" }, this.props.product.productImages && this.props.product.productImages.length > 0 ?
                React.createElement("div", { id: "carouselitem", className: "carousel slide carousel-fade", "data-ride": "carousel", "data-interval": "false" },
                    React.createElement("div", { className: "carousel-inner", role: "listbox" }, this.props.product.productImages.map(image => {
                        return (React.createElement(ProductImageComp, { image: image }));
                    })),
                    React.createElement("div", { className: "controlBlock" },
                        React.createElement("a", { className: "carousel-control-prev", href: "#carouselitem", role: "button", "data-slide": "prev" },
                            React.createElement("span", { className: "carousel-control-prev-icon", "aria-hidden": "true" }),
                            React.createElement("span", { className: "sr-only" }, "Previous")),
                        React.createElement("a", { className: "carousel-control-next", href: "#carouselitem", role: "button", "data-slide": "next" },
                            React.createElement("span", { className: "carousel-control-next-icon", "aria-hidden": "true" }),
                            React.createElement("span", { className: "sr-only" }, "Next")),
                        React.createElement("div", null,
                            React.createElement("ol", { className: "carousel-indicators" }, this.props.product.productImages.map(image => {
                                return (React.createElement(ProductImageController, { image: image }));
                            })))))
                : React.createElement("img", { style: { height: "300px" }, src: "/images/no-image.jpg" })),
            React.createElement("div", { className: "col-md-auto" },
                React.createElement("div", { style: { paddingLeft: '15px' } },
                    React.createElement("div", { className: "row" },
                        React.createElement("h3", null, "Select ur platform:")),
                    React.createElement("div", { className: "row" },
                        React.createElement("div", { className: "btn-group btn-group-toggle", "data-toggle": "buttons", style: { backgroundColor: "#29B6F6" }, id: "toggleGroup" },
                            React.createElement(ButtonGroupOwn, { inventories: this.props.product.inventories }))),
                    React.createElement(PriceDisplay, { inventory: GetInventory(this.props.product.inventories) }),
                    React.createElement("div", { className: "row" },
                        React.createElement("div", { style: { paddingRight: '10px' } },
                            React.createElement("button", { className: "btn btn-outline-primary rounded-circle btnClick", onClick: () => backend_1.AddToCart(this.props.product.productId, selectedPlatform, this.props.product.inventories) },
                                React.createElement("i", { className: "fa fa-shopping-cart " }))),
                        React.createElement("p", { style: { paddingTop: '5px' } }, "Add to shopping cart")),
                    userIsLoggedOn ? (React.createElement("div", { className: "row", style: { paddingTop: "5px" } },
                        React.createElement("div", { style: { paddingRight: '10px' } },
                            React.createElement("button", { className: "btn btn-outline-primary rounded-circle btnClick", onClick: () => backend_1.AddToWishList(this.props.product.productId, selectedPlatform, this.props.product.inventories) },
                                React.createElement("i", { className: "fa fa-list-ol " }))),
                        React.createElement("p", { style: { paddingTop: '5px' } }, "Add to my wishlist"))) :
                        React.createElement("div", { className: "row", style: { paddingTop: "5px" } },
                            React.createElement("div", { style: { paddingRight: '10px' } },
                                React.createElement("a", { className: "btn btn-outline-primary rounded-circle btnClick", href: "/WishList/AddToWishList" },
                                    React.createElement("i", { className: "fa fa-list-ol " }))),
                            React.createElement("p", { style: { paddingTop: '5px' } }, "Add to my wishlist"))))));
    }
}
class PriceDisplay extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (React.createElement("div", null,
            React.createElement("div", { className: "row", style: { paddingTop: '15px' } },
                React.createElement("div", { className: "col-auto; justify-content-start" },
                    React.createElement("p", { id: "priceDesc", className: "font-weight-bold", style: { fontSize: '25px' } },
                        '\u20AC',
                        this.props.inventory.price)),
                React.createElement("div", { className: "col-auto" },
                    React.createElement("div", { className: "inputWithIcon" },
                        React.createElement("div", { id: "stockDisplay" },
                            React.createElement(GetInnerDisplay, { gamePlatform: this.props.inventory.gamePlatform, inventoryId: this.props.inventory.inventoryId, platformId: this.props.inventory.platformId, price: this.props.inventory.price, productId: this.props.inventory.productId, stock: this.props.inventory.stock, product: null }))))),
            this.props.inventory.stock > 0 ? React.createElement("div", { className: "row", style: { marginTop: "-5px" } },
                React.createElement("p", null, "Ordered before 18:00 today, receive it tomorrow")) : null));
    }
}
class ButtonGroupOwn extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (React.createElement("div", { className: "btn-group" }, this.props.inventories.map(inventory => {
            if (inventory.platformId == platformIdToGet) {
                activePlatformSelect = false;
                return (React.createElement("label", { className: "btn btn-primary active", typeof: "button", onClick: () => ButtonGroupOwn.SetPlatform(inventory.platformId) },
                    React.createElement("input", { name: "options", type: "radio" }),
                    " ",
                    inventory.gamePlatform.platformName));
            }
            else {
                return (React.createElement("label", { className: "btn btn-primary", typeof: "button", onClick: () => ButtonGroupOwn.SetPlatform(inventory.platformId) },
                    React.createElement("input", { name: "options", type: "radio" }),
                    " ",
                    inventory.gamePlatform.platformName));
            }
        })));
    }
    static SetPlatform(id) {
        selectedPlatform = id;
        console.log(selectedPlatform + "\t" + productIdToGet);
        updateText(id);
    }
}
class ProductImageComp extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        if (activeImage) {
            activeImage = false;
            return (React.createElement("div", { className: "carousel-item active" },
                React.createElement("img", { className: "rounded embed-responsive-item", src: this.props.image.url, alt: "First slide" })));
        }
        else {
            return (React.createElement("div", { className: "carousel-item" },
                React.createElement("img", { className: "rounded embed-responsive-item", src: this.props.image.url, alt: "First slide" })));
        }
    }
}
class ProductImageController extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        if (activeController) {
            activeController = false;
            return (React.createElement("li", { "data-target": "#carouselitem", "data-slide-to": "0", className: "active" },
                React.createElement("img", { src: this.props.image.url })));
        }
        else {
            slideToNumber++;
            return (React.createElement("li", { "data-target": "#carouselitem", "data-slide-to": slideToNumber },
                React.createElement("img", { src: this.props.image.url })));
        }
    }
}
//function definitions
function GetInnerDisplay(inventory) {
    if (inventory.stock > 0) {
        return (React.createElement("div", null,
            React.createElement("p", { className: "text-success", style: { paddingRight: "20px", fontSize: '25px' } }, "In Stock"),
            React.createElement("i", { className: "fa fa-check", "aria-hidden": "true", style: { fontSize: "25px", position: "absolute", top: "6px" } })));
    }
    else {
        return (React.createElement("div", null,
            React.createElement("p", { className: "text-danger", style: { paddingRight: "15px", fontSize: "25px" } }, "Not In Stock"),
            React.createElement("i", { className: "fa fa-close", "aria-hidden": "true", style: { fontSize: "25px", position: "absolute", top: "8px", color: "rgb(220, 53, 69)" } })));
    }
}
function GetPlatformName(inventories, platformId) {
    for (let i = 0; i < inventories.length; i++) {
        if (selectedPlatform == inventories[i].platformId) {
            return inventories[i].gamePlatform.platformName;
        }
    }
    return "";
}
function GetInventory(inventories) {
    for (let i = 0; i < inventories.length; i++) {
        if (selectedPlatform == inventories[i].platformId) {
            return inventories[i];
        }
    }
}
var updateText = function (platformId) {
    activeController = true;
    activeImage = true;
    slideToNumber = 0;
    this.setState({ platformId: platformId });
};
var urlParams = new URLSearchParams(window.location.search);
var productName = urlParams.get('name');
var platformName = urlParams.get('platform');
console.log(productName + "\t" + platformName);
if (productName && platformName) {
    let jsonString = document.currentScript.getAttribute("JSON");
    let loggedOn = document.currentScript.getAttribute("LoggedOn") === "LoggedOn";
    if (loggedOn) {
        userIsLoggedOn = true;
    }
    let product = JSON.parse(jsonString);
    console.log(product);
    let platformId;
    for (let x = 0; x < product.inventories.length; x++) {
        if (platformName == product.inventories[x].gamePlatform.platformName) {
            platformId = product.inventories[x].platformId;
        }
    }
    if (platformId) {
        ReactDOM.render(React.createElement(TitleManager, { product: product, platformId: platformId }), document.getElementById("root"));
    }
}
//# sourceMappingURL=ProductPage.js.map