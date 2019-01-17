"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const ReactDOM = require("react-dom");
class SearchProductPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            products: false
        };
        this.filterProducts = this.filterProducts.bind(this);
        this.getProducts();
    }
    getProducts() {
        return __awaiter(this, void 0, void 0, function* () {
            var products;
            yield fetch("/Product/GetAllProducts").then(response => response.json()
                .then(json => {
                products = json;
                this.filterProducts(products);
            }));
        });
    }
    filterProducts(products) {
        let querystring = this.props.queryString.toLowerCase();
        let filteredProducts = [];
        // products.forEach(product=>{
        //     if(product.name.includes(this.props.queryString)){
        //         filteredProducts.push(product);
        //     }
        // });
        // products.forEach(product=>{
        //     product.productFeatures.forEach(feature =>{
        //         if(feature.feature.includes(this.props.queryString)) filteredProducts.push(product);
        //     })
        // });
        // products.forEach(product=>{
        //     if(product.publisher.publisherName.includes(this.props.queryString)) filteredProducts.push(product);
        // });
        // products.forEach(product=>{
        //     product.inventories.forEach(inventory =>{
        //         if(inventory.gamePlatform.platformName.includes(this.props.queryString)) filteredProducts.push(product)
        //     })
        // });
        for (let x = 0; x < products.length; x++) {
            if (products[x].name.toLowerCase().includes(querystring)) {
                filteredProducts.push(products[x]);
                products.splice(x, 1);
                x--;
            }
        }
        for (let x = 0; x < products.length; x++) {
            products[x].productFeatures.forEach(feature => {
                if (feature.feature.toLowerCase().includes(querystring)) {
                    filteredProducts.push(products[x]);
                    products.splice(x, 1);
                    x--;
                }
            });
        }
        for (let x = 0; x < products.length; x++) {
            if (products[x].publisher.publisherName.toLowerCase().includes(querystring)) {
                filteredProducts.push(products[x]);
                products.splice(x, 1);
                x--;
            }
        }
        for (let x = 0; x < products.length; x++) {
            products[x].inventories.forEach(inventory => {
                if (inventory.gamePlatform.platformName.toLowerCase().includes(querystring)) {
                    filteredProducts.push(products[x]);
                    products.splice(x, 1);
                    x--;
                }
            });
        }
        this.setState({ products: filteredProducts });
    }
    productContains(product, products) {
        products.forEach(item => {
            if (item.productId == product.productId)
                return true;
        });
        return false;
    }
    redirectTo() {
    }
    render() {
        if (this.state.products == false) {
            return (React.createElement("h1", null, "Loading"));
        }
        return (React.createElement("div", null,
            React.createElement("label", null,
                this.state.products.length,
                " search results for \"",
                this.props.queryString,
                "\""),
            React.createElement("div", { className: "row" },
                this.state.products.map(product => {
                    return (React.createElement("div", { className: "col-sm-6" },
                        React.createElement("div", { className: "row", style: { height: "150px" } },
                            React.createElement("div", { className: "col-sm-6" }, product.productImages != null && product.productImages.length > 0 ?
                                React.createElement("img", { src: product.productImages[0].url }) : React.createElement("img", { src: "/images/no-image.jpg" })),
                            React.createElement("div", { className: "col-sm-6" },
                                React.createElement("a", { href: "#" },
                                    React.createElement("h3", { style: { color: "black" } }, product.name)),
                                React.createElement("p", null, product.description))),
                        React.createElement("hr", null)));
                }),
                React.createElement("hr", null))));
    }
}
exports.SearchProductPage = SearchProductPage;
const pageLoad = (queryString) => {
    ReactDOM.render(React.createElement(SearchProductPage, { queryString: queryString }), document.getElementById("root"));
};
pageLoad(document.currentScript.getAttribute("querystring"));
//# sourceMappingURL=SearchProductPage.js.map