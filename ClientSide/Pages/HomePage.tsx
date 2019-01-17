import * as React from "react";
import * as ReactDOM from "react-dom";

class HomePage extends React.Component<{},{Loading : boolean}>{
    constructor(props : any){
        super(props);
        this.state = {
            Loading : false
        }
    }

    render() {
        if(this.state.Loading){
            return(
                <h1>Loading....</h1>
            )
        }
        return (
            <div>
                <div className="" style={{paddingTop:"10px"}}>
                    <div className="jumbotron" id="banner-main">
                        <div className="row">
                            <div className="col-lg-3">
                                <img className="card-img mt-3 ml-3 mb-3" src="images/test6.jpg"
                                     alt="No Image Available"/>
                            </div>
                            <div className="col-lg-9">
                                <h1>NEW: Call of Duty: Black Ops 4</h1>
                                <p className="lead">Call of Duty: Black Ops 4 (stylized as Call of Duty: Black Ops IIII)
                                    is a multiplayer first-person shooter developed by Treyarch and published by
                                    Activision.</p>
                                <a className="btn btn-light" href="/product?name=Call of Duty Black Ops 4&platform=PS4">Visit product
                                    page</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

ReactDOM.render(
    <HomePage/>,
    document.getElementById("root")
);