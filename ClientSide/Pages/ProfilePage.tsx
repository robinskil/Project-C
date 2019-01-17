import * as React from 'react';
import User from "../Models/User";
import {Order} from "../Models/Order";
import * as ReactDOM from "react-dom";


const row = (key: string, value: any, modal:string = "") => (
    <div className="col-8">
        <div className="row">
            <div className="col-8">
                <div className="row">
                    <div className="col-6">
                        <strong>{key}</strong>
                    </div>
                    <div className="col-6">
                        <p className="text-right">{value}</p>
                    </div>
                </div>
                <hr style={{marginTop: "0"}}/>
            </div>
            {modal != "" ?
                <div className="col-4">
                    <button className="btn btn-primary" data-toggle="modal" data-target={"#"+modal}>Change</button>
                </div> : null
            }
        </div>
    </div>);

export class ProfilePage extends React.Component<{ user: User }, { Orders: Order[], ToggleOrderInfo: Order }> {
    constructor(props: any) {
        super(props);
        this.f = this.f.bind(this);
        this.getOrders = this.getOrders.bind(this);
        this.ToggleOrderInfo = this.ToggleOrderInfo.bind(this);
        this.ToggleOrderInfoOff = this.ToggleOrderInfoOff.bind(this);
        this.getOrders();
        this.state = {
            Orders: null,
            ToggleOrderInfo: null
        };
    }

    f(): void {
        console.log("Test");
    }
    async getOrders() {
        await fetch("/Order/GetOrders", {
            credentials: "same-origin"
        }).then(response => {
            response.json().then(json => {
                this.setState({Orders: json});
            })
        })
    }

    ToggleOrderInfo(order: Order) {
        this.setState({ToggleOrderInfo: order})
    }

    ToggleOrderInfoOff() {
        this.setState({ToggleOrderInfo: null});
    }

    render() {
        return (
            <div>
                <h3>Your Account</h3>
                <h4 className="text-primary">Personal Information</h4>
                {row("Invoice billed to", this.props.user.name + " " + this.props.user.surname, "userInfoModal")}
                <div className="modal fade" id="userInfoModal" tabIndex={-1} role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                    <div className="modal-dialog modal-dialog-centered" role="document">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title" id="exampleModalLongTitle">Change Name and Surname</h5>
                                <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        <div className="modal-body">
                            <form className="needs-validation" id="ChangeUserInfoForm" method="POST" action="/User/ChangeName" noValidate>
                                <div className="form-group row">
                                    <label htmlFor="Name" className="col-sm-4 col-form-label">New First Name</label>
                                    <div className="col-sm-8">
                                    <input type="text" className="form-control" id="Name" placeholder="First Name" name="Name" required maxLength={30}/>
                                        <div className="valid-feedback">
                                            Looks good!
                                        </div>
                                        <div className="invalid-feedback">
                                            This field is required and cannot be longer than 30 characters.
                                        </div>
                                    </div>
                                </div>
                                <div className="form-group row">
                                    <label htmlFor="Surname" className="col-sm-4 col-form-label">New Surname</label>
                                    <div className="col-sm-8">
                                    <input type="text" className="form-control" id="Surname" placeholder="Surname" name="Surname" required maxLength={30}/>
                                        <div className="valid-feedback">
                                            Looks good!
                                        </div>
                                        <div className="invalid-feedback">
                                            This field is required and cannot be longer than 30 characters.
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-secondary" data-dismiss="modal">Close</button>
                                <button type="submit" form="ChangeUserInfoForm" className="btn btn-primary">Save changes</button>
                            </div>
                        </div>
                    </div>
                </div>
                {row("Email address", this.props.user.email)}
                {row("Billing address", this.props.user.street + " " + this.props.user.postalCode, "addressModal")}
                <div className="modal fade" id="addressModal" tabIndex={-1} role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                    <div className="modal-dialog modal-dialog-centered" role="document">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title" id="exampleModalLongTitle">Change Address</h5>
                                <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        <div className="modal-body">
                            <form className="needs-validation" id="ChangeAddressForm" action="/User/ChangeAddress" method="POST" noValidate>
                                <div className="form-group row">
                                    <label htmlFor="Street" className="col-sm-4 col-form-label">New Street</label>
                                    <div className="col-sm-8">
                                    <input type="text" className="form-control" id="Street" placeholder="Street" name="Street" required maxLength={200}/>
                                        <div className="valid-feedback">
                                            Looks good!
                                        </div>
                                        <div className="invalid-feedback">
                                            This field is required and cannot be longer than 200 characters.
                                        </div>
                                    </div>
                                </div>
                                <div className="form-group row">
                                    <label htmlFor="PostalCode" className="col-sm-4 col-form-label">New Postal Code</label>
                                    <div className="col-sm-8">
                                    <input type="text" className="form-control" id="PostalCode" placeholder="Postal Code" name="PostalCode" required maxLength={6} minLength={6}/>
                                        <div className="valid-feedback">
                                            Looks good!
                                        </div>
                                        <div className="invalid-feedback">
                                            This field is required and contains 6 characters.
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-secondary" data-dismiss="modal">Close</button>
                                <button type="submit" form="ChangeAddressForm" className="btn btn-primary">Save changes</button>
                            </div>
                        </div>
                    </div>
                </div>
                {row("Birthdate", this.props.user.birthdate.substr(0, 10))}
                {row("Password" , "●●●●●●●●" , "passwordModal")}
                <div className="modal fade" id="passwordModal" tabIndex={-1} role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                    <div className="modal-dialog modal-dialog-centered" role="document">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title" id="exampleModalLongTitle">Change password</h5>
                                <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        <div className="modal-body">
                            <form className="needs-validation" id="ChangePasswordForm" action="/User/ChangePassword" method="POST" noValidate>
                                <div className="form-group row">
                                    <label htmlFor="Password" className="col-sm-4 col-form-label">New Password</label>
                                    <div className="col-sm-8">
                                    <input type="password" className="form-control" id="Password" placeholder="Password" name="Password" required minLength={8} maxLength={200} pattern={"^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$"}/>
                                        <div className="valid-feedback">
                                            Looks good!
                                        </div>
                                        <div className="invalid-feedback">
                                            Password must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-secondary" data-dismiss="modal">Close</button>
                                <button type="submit" form="ChangePasswordForm" className="btn btn-primary">Save changes</button>
                            </div>
                        </div>
                    </div>
                </div>
                {this.state.Orders != null ?
                    <div>
                        <table className="table table-borderless table-hover tableCustom slide-in-blurred-top">
                            <thead className="table-head-custom">
                            <tr>
                                <th scope="col">Date</th>
                                <th scope="col">Address</th>
                                <th scope="col">Order status</th>
                                <th scope="col">Total Price</th>
                            </tr>
                            </thead>
                            <tbody>
                            {this.state.Orders.map(order => {
                                    return (
                                        <OrderRow order={order} ToggleOrderInfo={this.ToggleOrderInfo}/>
                                    )
                                }
                            )}
                            </tbody>
                        </table>
                    </div> : null
                }
                {this.state.ToggleOrderInfo ?
                    <OrderView order={this.state.ToggleOrderInfo} ToggleOrderInfoOff={this.ToggleOrderInfoOff}/> : null}
            </div>
        )
    }
}

class OrderRow extends React.Component<{ order: Order, ToggleOrderInfo: (order: Order) => any }> {
    constructor(props: any) {
        super(props);
        console.log(this.props.order + "is Order row order")
    }

    GetTotalPrice(): string {
        let total = 0;
        for (let x = 0; x < this.props.order.orderItems.length; x++) {
            total += this.props.order.orderItems[x].priceBought * this.props.order.orderItems[x].amount;
        }
        return total.toFixed(2);
    }

    render() {
        if (this.props.order.orderStatus.statusId == 4) {
            return (
                <tr className="table-light pointer" onClick={() => this.props.ToggleOrderInfo(this.props.order)}>
                    <th scope="row">{this.props.order.date.substr(0, 10)}</th>
                    <td>{this.props.order.street + " " + this.props.order.postalCode}</td>
                    <td>{this.props.order.orderStatus.statusName}</td>
                    <td>€{this.GetTotalPrice()}</td>
                </tr>
            )
        }
        else if (this.props.order.orderStatus.statusId == 3) {
            return (
                <tr className="table-light pointer" onClick={() => this.props.ToggleOrderInfo(this.props.order)}>
                    <th scope="row">{this.props.order.date.substr(0, 10)}</th>
                    <td>{this.props.order.street + " " + this.props.order.postalCode}</td>
                    <td>{this.props.order.orderStatus.statusName}</td>
                    <td>€{this.GetTotalPrice()}</td>
                </tr>
            )
        }
        else if (this.props.order.orderStatus.statusId == 2) {
            return (
                <tr className="table-light pointer" onClick={() => this.props.ToggleOrderInfo(this.props.order)}>
                    <th scope="row">{this.props.order.date.substr(0, 10)}</th>
                    <td>{this.props.order.street + " " + this.props.order.postalCode}</td>
                    <td>{this.props.order.orderStatus.statusName}</td>
                    <td>€{this.GetTotalPrice()}</td>
                </tr>
            )
        }
        else if (this.props.order.orderStatus.statusId == 1) {
            return (
                <tr className="table-light pointer" onClick={() => this.props.ToggleOrderInfo(this.props.order)}>
                    <th scope="row">{this.props.order.date.substr(0, 10)}</th>
                    <td>{this.props.order.street + " " + this.props.order.postalCode}</td>
                    <td>{this.props.order.orderStatus.statusName}</td>
                    <td>€{this.GetTotalPrice()}</td>
                </tr>
            )
        }
    }
}

class OrderView extends React.Component<{ order: Order, ToggleOrderInfoOff: () => any }> {
    constructor(props: any) {
        super(props);
    }

    render() {
        return (
            <div className="popup slide-in-fwd-center" >
                <OrderInfo ToggleOrderInfoOff={this.props.ToggleOrderInfoOff} order={this.props.order} />
            </div>
        )
    }
}

class OrderInfo extends React.Component<{ order: Order, ToggleOrderInfoOff: () => any }> {
    constructor(props: any) {
        super(props);
    }

    render() {
        return (
            <div className="popUpOrderInner slide-in-fwd-center" style={{overflowY:"scroll" , overflowX:"hidden"}} >
                {this.props.order.orderItems.map(orderItem => {
                        if (orderItem.inventory != null && orderItem.inventory.product != null) {
                            return (
                                <div>
                                    <div className="row popRowMargin">
                                        <div className="col-sm-3">
                                            {orderItem.inventory.product.productImages[0] != null ?
                                                <img src={orderItem.inventory.product.productImages[0].url}/>
                                                : null}
                                        </div>
                                        <div className="col-sm-4">
                                            <h4>{orderItem.inventory.product.name} - {orderItem.inventory.gamePlatform.platformName}</h4>
                                            <p>
                                                {orderItem.inventory.product.description}
                                            </p>
                                        </div>
                                        <div className="col-sm-2 centerTextBlock">
                                            <h6>Amount : {orderItem.amount}</h6>
                                        </div>
                                        <div className="col-sm-2 centerTextBlock">
                                            <h6>€{(orderItem.amount * orderItem.priceBought).toFixed(2)}</h6>
                                        </div>
                                    </div>
                                    <hr/>
                                </div>
                            )
                        }
                        return (
                            <div>
                                <h1>Failed</h1>
                            </div>
                        )
                    }
                )}
            </div>
        )
    }

    componentDidMount() {
        document.addEventListener('click', this.handleClickOutside, true);
    }

    componentWillUnmount() {
        document.removeEventListener('click', this.handleClickOutside, true);
    }

    handleClickOutside = (event: any) => {
        const domNode = ReactDOM.findDOMNode(this);

        if (!domNode || !domNode.contains(event.target)) {
            this.props.ToggleOrderInfoOff();
        }
    }
}

function LoadPage() {
    let val: string = document.currentScript.getAttribute("jsonString");
    console.log(val);
    let user: User = JSON.parse(val);
    ReactDOM.render(
        <ProfilePage user={user}/>,
        document.getElementById("root")
    );
}

LoadPage();

