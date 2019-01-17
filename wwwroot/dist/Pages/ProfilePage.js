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
const row = (key, value, modal = "") => (React.createElement("div", { className: "col-8" },
    React.createElement("div", { className: "row" },
        React.createElement("div", { className: "col-8" },
            React.createElement("div", { className: "row" },
                React.createElement("div", { className: "col-6" },
                    React.createElement("strong", null, key)),
                React.createElement("div", { className: "col-6" },
                    React.createElement("p", { className: "text-right" }, value))),
            React.createElement("hr", { style: { marginTop: "0" } })),
        modal != "" ?
            React.createElement("div", { className: "col-4" },
                React.createElement("button", { className: "btn btn-primary", "data-toggle": "modal", "data-target": "#" + modal }, "Change")) : null)));
class ProfilePage extends React.Component {
    constructor(props) {
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
    f() {
        console.log("Test");
    }
    getOrders() {
        return __awaiter(this, void 0, void 0, function* () {
            yield fetch("/Order/GetOrders", {
                credentials: "same-origin"
            }).then(response => {
                response.json().then(json => {
                    this.setState({ Orders: json });
                });
            });
        });
    }
    ToggleOrderInfo(order) {
        this.setState({ ToggleOrderInfo: order });
    }
    ToggleOrderInfoOff() {
        this.setState({ ToggleOrderInfo: null });
    }
    render() {
        return (React.createElement("div", null,
            React.createElement("h3", null, "Your Account"),
            React.createElement("h4", { className: "text-primary" }, "Personal Information"),
            row("Invoice billed to", this.props.user.name + " " + this.props.user.surname, "userInfoModal"),
            React.createElement("div", { className: "modal fade", id: "userInfoModal", tabIndex: -1, role: "dialog", "aria-labelledby": "exampleModalCenterTitle", "aria-hidden": "true" },
                React.createElement("div", { className: "modal-dialog modal-dialog-centered", role: "document" },
                    React.createElement("div", { className: "modal-content" },
                        React.createElement("div", { className: "modal-header" },
                            React.createElement("h5", { className: "modal-title", id: "exampleModalLongTitle" }, "Change Name and Surname"),
                            React.createElement("button", { type: "button", className: "close", "data-dismiss": "modal", "aria-label": "Close" },
                                React.createElement("span", { "aria-hidden": "true" }, "\u00D7"))),
                        React.createElement("div", { className: "modal-body" },
                            React.createElement("form", { className: "needs-validation", id: "ChangeUserInfoForm", method: "POST", action: "/User/ChangeName", noValidate: true },
                                React.createElement("div", { className: "form-group row" },
                                    React.createElement("label", { htmlFor: "Name", className: "col-sm-4 col-form-label" }, "New First Name"),
                                    React.createElement("div", { className: "col-sm-8" },
                                        React.createElement("input", { type: "text", className: "form-control", id: "Name", placeholder: "First Name", name: "Name", required: true, maxLength: 30 }),
                                        React.createElement("div", { className: "valid-feedback" }, "Looks good!"),
                                        React.createElement("div", { className: "invalid-feedback" }, "This field is required and cannot be longer than 30 characters."))),
                                React.createElement("div", { className: "form-group row" },
                                    React.createElement("label", { htmlFor: "Surname", className: "col-sm-4 col-form-label" }, "New Surname"),
                                    React.createElement("div", { className: "col-sm-8" },
                                        React.createElement("input", { type: "text", className: "form-control", id: "Surname", placeholder: "Surname", name: "Surname", required: true, maxLength: 30 }),
                                        React.createElement("div", { className: "valid-feedback" }, "Looks good!"),
                                        React.createElement("div", { className: "invalid-feedback" }, "This field is required and cannot be longer than 30 characters."))))),
                        React.createElement("div", { className: "modal-footer" },
                            React.createElement("button", { type: "button", className: "btn btn-secondary", "data-dismiss": "modal" }, "Close"),
                            React.createElement("button", { type: "submit", form: "ChangeUserInfoForm", className: "btn btn-primary" }, "Save changes"))))),
            row("Email address", this.props.user.email),
            row("Billing address", this.props.user.street + " " + this.props.user.postalCode, "addressModal"),
            React.createElement("div", { className: "modal fade", id: "addressModal", tabIndex: -1, role: "dialog", "aria-labelledby": "exampleModalCenterTitle", "aria-hidden": "true" },
                React.createElement("div", { className: "modal-dialog modal-dialog-centered", role: "document" },
                    React.createElement("div", { className: "modal-content" },
                        React.createElement("div", { className: "modal-header" },
                            React.createElement("h5", { className: "modal-title", id: "exampleModalLongTitle" }, "Change Address"),
                            React.createElement("button", { type: "button", className: "close", "data-dismiss": "modal", "aria-label": "Close" },
                                React.createElement("span", { "aria-hidden": "true" }, "\u00D7"))),
                        React.createElement("div", { className: "modal-body" },
                            React.createElement("form", { className: "needs-validation", id: "ChangeAddressForm", action: "/User/ChangeAddress", method: "POST", noValidate: true },
                                React.createElement("div", { className: "form-group row" },
                                    React.createElement("label", { htmlFor: "Street", className: "col-sm-4 col-form-label" }, "New Street"),
                                    React.createElement("div", { className: "col-sm-8" },
                                        React.createElement("input", { type: "text", className: "form-control", id: "Street", placeholder: "Street", name: "Street", required: true, maxLength: 200 }),
                                        React.createElement("div", { className: "valid-feedback" }, "Looks good!"),
                                        React.createElement("div", { className: "invalid-feedback" }, "This field is required and cannot be longer than 200 characters."))),
                                React.createElement("div", { className: "form-group row" },
                                    React.createElement("label", { htmlFor: "PostalCode", className: "col-sm-4 col-form-label" }, "New Postal Code"),
                                    React.createElement("div", { className: "col-sm-8" },
                                        React.createElement("input", { type: "text", className: "form-control", id: "PostalCode", placeholder: "Postal Code", name: "PostalCode", required: true, maxLength: 6, minLength: 6 }),
                                        React.createElement("div", { className: "valid-feedback" }, "Looks good!"),
                                        React.createElement("div", { className: "invalid-feedback" }, "This field is required and contains 6 characters."))))),
                        React.createElement("div", { className: "modal-footer" },
                            React.createElement("button", { type: "button", className: "btn btn-secondary", "data-dismiss": "modal" }, "Close"),
                            React.createElement("button", { type: "submit", form: "ChangeAddressForm", className: "btn btn-primary" }, "Save changes"))))),
            row("Birthdate", this.props.user.birthdate.substr(0, 10)),
            row("Password", "●●●●●●●●", "passwordModal"),
            React.createElement("div", { className: "modal fade", id: "passwordModal", tabIndex: -1, role: "dialog", "aria-labelledby": "exampleModalCenterTitle", "aria-hidden": "true" },
                React.createElement("div", { className: "modal-dialog modal-dialog-centered", role: "document" },
                    React.createElement("div", { className: "modal-content" },
                        React.createElement("div", { className: "modal-header" },
                            React.createElement("h5", { className: "modal-title", id: "exampleModalLongTitle" }, "Change password"),
                            React.createElement("button", { type: "button", className: "close", "data-dismiss": "modal", "aria-label": "Close" },
                                React.createElement("span", { "aria-hidden": "true" }, "\u00D7"))),
                        React.createElement("div", { className: "modal-body" },
                            React.createElement("form", { className: "needs-validation", id: "ChangePasswordForm", action: "/User/ChangePassword", method: "POST", noValidate: true },
                                React.createElement("div", { className: "form-group row" },
                                    React.createElement("label", { htmlFor: "Password", className: "col-sm-4 col-form-label" }, "New Password"),
                                    React.createElement("div", { className: "col-sm-8" },
                                        React.createElement("input", { type: "password", className: "form-control", id: "Password", placeholder: "Password", name: "Password", required: true, minLength: 8, maxLength: 200, pattern: "^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$" }),
                                        React.createElement("div", { className: "valid-feedback" }, "Looks good!"),
                                        React.createElement("div", { className: "invalid-feedback" }, "Password must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)"))))),
                        React.createElement("div", { className: "modal-footer" },
                            React.createElement("button", { type: "button", className: "btn btn-secondary", "data-dismiss": "modal" }, "Close"),
                            React.createElement("button", { type: "submit", form: "ChangePasswordForm", className: "btn btn-primary" }, "Save changes"))))),
            this.state.Orders != null ?
                React.createElement("div", null,
                    React.createElement("table", { className: "table table-borderless table-hover tableCustom slide-in-blurred-top" },
                        React.createElement("thead", { className: "table-head-custom" },
                            React.createElement("tr", null,
                                React.createElement("th", { scope: "col" }, "Date"),
                                React.createElement("th", { scope: "col" }, "Address"),
                                React.createElement("th", { scope: "col" }, "Order status"),
                                React.createElement("th", { scope: "col" }, "Total Price"))),
                        React.createElement("tbody", null, this.state.Orders.map(order => {
                            return (React.createElement(OrderRow, { order: order, ToggleOrderInfo: this.ToggleOrderInfo }));
                        })))) : null,
            this.state.ToggleOrderInfo ?
                React.createElement(OrderView, { order: this.state.ToggleOrderInfo, ToggleOrderInfoOff: this.ToggleOrderInfoOff }) : null));
    }
}
exports.ProfilePage = ProfilePage;
class OrderRow extends React.Component {
    constructor(props) {
        super(props);
        console.log(this.props.order + "is Order row order");
    }
    GetTotalPrice() {
        let total = 0;
        for (let x = 0; x < this.props.order.orderItems.length; x++) {
            total += this.props.order.orderItems[x].priceBought * this.props.order.orderItems[x].amount;
        }
        return total.toFixed(2);
    }
    render() {
        if (this.props.order.orderStatus.statusId == 4) {
            return (React.createElement("tr", { className: "table-light pointer", onClick: () => this.props.ToggleOrderInfo(this.props.order) },
                React.createElement("th", { scope: "row" }, this.props.order.date.substr(0, 10)),
                React.createElement("td", null, this.props.order.street + " " + this.props.order.postalCode),
                React.createElement("td", null, this.props.order.orderStatus.statusName),
                React.createElement("td", null,
                    "\u20AC",
                    this.GetTotalPrice())));
        }
        else if (this.props.order.orderStatus.statusId == 3) {
            return (React.createElement("tr", { className: "table-light pointer", onClick: () => this.props.ToggleOrderInfo(this.props.order) },
                React.createElement("th", { scope: "row" }, this.props.order.date.substr(0, 10)),
                React.createElement("td", null, this.props.order.street + " " + this.props.order.postalCode),
                React.createElement("td", null, this.props.order.orderStatus.statusName),
                React.createElement("td", null,
                    "\u20AC",
                    this.GetTotalPrice())));
        }
        else if (this.props.order.orderStatus.statusId == 2) {
            return (React.createElement("tr", { className: "table-light pointer", onClick: () => this.props.ToggleOrderInfo(this.props.order) },
                React.createElement("th", { scope: "row" }, this.props.order.date.substr(0, 10)),
                React.createElement("td", null, this.props.order.street + " " + this.props.order.postalCode),
                React.createElement("td", null, this.props.order.orderStatus.statusName),
                React.createElement("td", null,
                    "\u20AC",
                    this.GetTotalPrice())));
        }
        else if (this.props.order.orderStatus.statusId == 1) {
            return (React.createElement("tr", { className: "table-light pointer", onClick: () => this.props.ToggleOrderInfo(this.props.order) },
                React.createElement("th", { scope: "row" }, this.props.order.date.substr(0, 10)),
                React.createElement("td", null, this.props.order.street + " " + this.props.order.postalCode),
                React.createElement("td", null, this.props.order.orderStatus.statusName),
                React.createElement("td", null,
                    "\u20AC",
                    this.GetTotalPrice())));
        }
    }
}
class OrderView extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (React.createElement("div", { className: "popup slide-in-fwd-center" },
            React.createElement(OrderInfo, { ToggleOrderInfoOff: this.props.ToggleOrderInfoOff, order: this.props.order })));
    }
}
class OrderInfo extends React.Component {
    constructor(props) {
        super(props);
        this.handleClickOutside = (event) => {
            const domNode = ReactDOM.findDOMNode(this);
            if (!domNode || !domNode.contains(event.target)) {
                this.props.ToggleOrderInfoOff();
            }
        };
    }
    render() {
        return (React.createElement("div", { className: "popUpOrderInner slide-in-fwd-center", style: { overflowY: "scroll", overflowX: "hidden" } }, this.props.order.orderItems.map(orderItem => {
            if (orderItem.inventory != null && orderItem.inventory.product != null) {
                return (React.createElement("div", null,
                    React.createElement("div", { className: "row popRowMargin" },
                        React.createElement("div", { className: "col-sm-3" }, orderItem.inventory.product.productImages[0] != null ?
                            React.createElement("img", { src: orderItem.inventory.product.productImages[0].url })
                            : null),
                        React.createElement("div", { className: "col-sm-4" },
                            React.createElement("h4", null,
                                orderItem.inventory.product.name,
                                " - ",
                                orderItem.inventory.gamePlatform.platformName),
                            React.createElement("p", null, orderItem.inventory.product.description)),
                        React.createElement("div", { className: "col-sm-2 centerTextBlock" },
                            React.createElement("h6", null,
                                "Amount : ",
                                orderItem.amount)),
                        React.createElement("div", { className: "col-sm-2 centerTextBlock" },
                            React.createElement("h6", null,
                                "\u20AC",
                                (orderItem.amount * orderItem.priceBought).toFixed(2)))),
                    React.createElement("hr", null)));
            }
            return (React.createElement("div", null,
                React.createElement("h1", null, "Failed")));
        })));
    }
    componentDidMount() {
        document.addEventListener('click', this.handleClickOutside, true);
    }
    componentWillUnmount() {
        document.removeEventListener('click', this.handleClickOutside, true);
    }
}
function LoadPage() {
    let val = document.currentScript.getAttribute("jsonString");
    console.log(val);
    let user = JSON.parse(val);
    ReactDOM.render(React.createElement(ProfilePage, { user: user }), document.getElementById("root"));
}
LoadPage();
//# sourceMappingURL=ProfilePage.js.map