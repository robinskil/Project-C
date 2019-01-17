import * as React from 'react';
import { Product } from '../Models/Product';
import { throws } from 'assert';
import * as ReactDOM from "react-dom";

export class SearchProductPage extends React.Component<{queryString : string}, {products : Product[] | false}> {
    constructor(props:any){
        super(props);
        this.state = {
            products : false
        };
        this.filterProducts = this.filterProducts.bind(this);
        this.getProducts();
    }
    async getProducts(){
        var products : Product[];
        await fetch("/Product/GetAllProducts").then(response => response.json()
        .then(json => {
            products = json;
            this.filterProducts(products);
        }))
    }
    filterProducts(products : Product[]){
        let querystring = this.props.queryString.toLowerCase();
        let filteredProducts : Product[] = [];
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
        for(let x = 0; x < products.length ; x++) {
            if(products[x].name.toLowerCase().includes(querystring)){
                filteredProducts.push(products[x]);
                products.splice(x , 1);
                x--;
            }
        }
        for(let x = 0; x < products.length ; x++) {
            products[x].productFeatures.forEach(feature =>{
                if(feature.feature.toLowerCase().includes(querystring)){
                    filteredProducts.push(products[x]);
                    products.splice(x , 1);
                    x--;
                }
            })
        }
        for(let x = 0; x < products.length ; x++) {
            if(products[x].publisher.publisherName.toLowerCase().includes(querystring)){
                filteredProducts.push(products[x]);
                products.splice(x , 1);
                x--;
            }
        }
        for(let x = 0; x < products.length ; x++) {
            products[x].inventories.forEach(inventory =>{
                if(inventory.gamePlatform.platformName.toLowerCase().includes(querystring)){
                    filteredProducts.push(products[x]);
                    products.splice(x , 1);
                    x--;
                }
            })
        }
        this.setState({products:filteredProducts});
    }
    productContains(product : Product , products : Product[]){
        products.forEach(item => {
            if (item.productId == product.productId) return true;
        });
        return false;
    }
    redirectTo(){
        
    }
    render() {
        if(this.state.products == false){
            return (<h1>Loading</h1>);
        }
        return (
            <div>
                <label>{this.state.products.length} search results for "{this.props.queryString}"</label>
                <div className="row">
                    {this.state.products.map(product => {
                        return (
                            <div className="col-sm-6" >
                                <div className="row" style={{height:"150px"}}>
                                    <div className="col-sm-6" >
                                        {product.productImages != null && product.productImages.length > 0 ?
                                            <img src={product.productImages[0].url} /> : <img src="/images/no-image.jpg"/>}
                                    </div>
                                    <div className="col-sm-6">
                                        <a href="#"><h3 style={{color:"black"}}>{product.name}</h3></a>
                                        <p>{product.description}</p>
                                    </div>
                                </div>
                                <hr/>
                            </div>
                        )
                    })}
                    <hr/>
                </div>
            </div>
        )
    }
}

const pageLoad = (queryString : string) =>{
    ReactDOM.render(
        <SearchProductPage queryString={queryString}/>,
        document.getElementById("root")
    );
};

pageLoad(document.currentScript.getAttribute("querystring"));