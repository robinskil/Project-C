"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const ReactDOM = require("react-dom");
const RegisterBackend_1 = require("../BackEndClientSide/RegisterBackend");
class LoginPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            email: '',
            password: ''
        };
        this.handleEmail = this.handleEmail.bind(this);
        this.handlePassword = this.handlePassword.bind(this);
    }
    handleEmail(event) {
        this.setState({ email: event.target.value });
    }
    handlePassword(event) {
        this.setState({ password: event.target.value });
    }
    Login() {
        console.log(this.state.email + "\t" + this.state.password);
        if (this.CheckValidation()) {
            let data = new FormData();
            data.append("email", this.state.email);
            data.append("password", this.state.password);
            let url = '/User/LoginUser';
            let response = fetch(url, {
                method: "POST",
                headers: {
                    'Accept': 'application/json, application/xml, text/plain, text/html, *.*'
                },
                body: data
            });
            response.then(response => {
                if (response.ok) {
                    console.log(response.text());
                    window.location.href = "/";
                }
                else {
                    console.log("Failed login");
                    //TODO : if failed return a error message
                }
            });
        }
    }
    CheckValidation() {
        return RegisterBackend_1.CheckValidityInputElement("loginInputEmail") && RegisterBackend_1.CheckValidityInputElement("loginInputPassword");
    }
    render() {
        return (React.createElement("div", { style: { paddingTop: "20px" } },
            React.createElement("div", { className: "d-flex justify-content-center" },
                React.createElement("div", null,
                    React.createElement("h3", null, "Login"),
                    React.createElement("hr", { style: { marginBottom: "8px" } }),
                    React.createElement("div", { className: "row" },
                        React.createElement("div", { className: "col-sm-12 col-md-6", style: { borderRight: "1px solid rgba(0, 0, 0, 0.1)", marginTop: "-8.3px" } },
                            React.createElement("form", { style: { marginTop: "13px" } },
                                React.createElement("div", { className: "form-group" },
                                    React.createElement("input", { type: "email", className: "form-control", id: "loginInputEmail", required: true, minLength: 1, "aria-describedby": "emailHelp", placeholder: "Email address", onChange: this.handleEmail, value: this.state.email })),
                                React.createElement("div", { className: "form-group" },
                                    React.createElement("input", { type: "password", className: "form-control", id: "loginInputPassword", required: true, minLength: 1, placeholder: "Password", onChange: this.handlePassword, value: this.state.password })),
                                React.createElement("button", { type: "button", onClick: () => this.Login(), className: "btn btn-primary" }, "Login"))),
                        React.createElement("div", { className: "col-sm-12 col-md-6" },
                            React.createElement("label", { style: { marginTop: "10px" } }, "Don't have an account yet?"),
                            React.createElement("a", { style: { marginTop: "17px" }, className: "btn btn-primary", href: "/Register" }, "Create a new account")))))));
    }
}
ReactDOM.render(React.createElement(LoginPage, null), document.getElementById("root"));
//# sourceMappingURL=LoginPage.js.map