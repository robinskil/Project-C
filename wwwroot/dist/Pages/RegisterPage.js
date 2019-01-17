"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const RegisterBackend_1 = require("../BackEndClientSide/RegisterBackend");
const User_1 = require("../Models/User");
const ReactDOM = require("react-dom");
class RegisterPage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            UserObject: new User_1.default(),
            Registering: false
        };
        this.handleEmail = this.handleEmail.bind(this);
        this.handleFirstName = this.handleFirstName.bind(this);
        this.handleSurName = this.handleSurName.bind(this);
        this.handleStreetName = this.handleStreetName.bind(this);
        this.handlePostalCode = this.handlePostalCode.bind(this);
        this.handleDateOfBirth = this.handleDateOfBirth.bind(this);
        this.handlePassword = this.handlePassword.bind(this);
        this.Register = this.Register.bind(this);
    }
    handleEmail(event) {
        // this.setState({UserObject:{...this.state.UserObject,Email:event.target.value}});
        RegisterBackend_1.SetCustomValidityElement("registerEmail", "");
        this.state.UserObject.email = event.target.value;
        console.log(this.state.UserObject.email);
    }
    handleFirstName(event) {
        this.state.UserObject.name = event.target.value;
    }
    handleSurName(event) {
        this.state.UserObject.surname = event.target.value;
    }
    handleStreetName(event) {
        this.state.UserObject.street = event.target.value;
    }
    handlePostalCode(event) {
        this.state.UserObject.postalCode = event.target.value;
    }
    handleDateOfBirth(event) {
        this.state.UserObject.birthdate = event.target.value;
    }
    handlePassword(event) {
        this.state.UserObject.password = event.target.value;
    }
    Register(event) {
        //if all fields are validated
        if (this.CheckValidation()) {
            event.preventDefault();
            let url = '/User/Register';
            let response = fetch(url, {
                method: "POST",
                headers: {
                    'Accept': 'application/json, application/xml, text/plain, text/html, *.*',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(this.state.UserObject)
            });
            response.then(response => {
                if (response.ok) {
                    console.log(response.text());
                    window.location.href = "/Login";
                }
                else {
                    response.json().then(json => this.SetValidation(json));
                }
            });
        }
    }
    SetValidation(errors) {
        for (let i = 0; i < errors.length; i++) {
            if (errors[i] == 0) {
                RegisterBackend_1.SetCustomValidityElement("registerEmail", "Email already exists");
            }
            else {
                RegisterBackend_1.SetCustomValidityElement("registerEmail", "Something went wrong.....");
            }
        }
    }
    CheckValidation() {
        return RegisterBackend_1.CheckValidityInputElement("registerEmail") && RegisterBackend_1.CheckValidityInputElement("registerPassword") &&
            RegisterBackend_1.CheckValidityInputElement("registerFirstName") && RegisterBackend_1.CheckValidityInputElement("registerSurName") &&
            RegisterBackend_1.CheckValidityInputElement("registerStreetName") && RegisterBackend_1.CheckValidityInputElement("registerPostalCode") &&
            RegisterBackend_1.CheckValidityInputElement("registerDateOfBirth");
    }
    render() {
        return (React.createElement("div", { style: { paddingTop: "20px" } },
            React.createElement("div", { className: "d-flex justify-content-center" },
                React.createElement("div", null,
                    React.createElement("h3", null, "Create a new account"),
                    React.createElement("hr", null),
                    React.createElement("div", { className: "row" },
                        React.createElement("div", { className: "col-12" },
                            React.createElement("form", null,
                                React.createElement("div", { className: "form-group" },
                                    React.createElement("input", { type: "email", className: "form-control", id: "registerEmail", required: true, "aria-describedby": "emailHelp", placeholder: "Email address", onChange: this.handleEmail, value: this.state.UserObject.email }),
                                    React.createElement("small", { id: "emailHelp", className: "form-text text-muted" }, "We'll never share your email with anyone else.")),
                                React.createElement("div", { className: "form-group" },
                                    React.createElement("input", { type: "password", className: "form-control", id: "registerPassword", required: true, placeholder: "Password", onChange: this.handlePassword, value: this.state.UserObject.password })),
                                React.createElement("div", { className: "form-group" },
                                    React.createElement("input", { type: "text", className: "form-control", placeholder: "First name", id: "registerFirstName", onChange: this.handleFirstName, value: this.state.UserObject.name, required: true })),
                                React.createElement("div", { className: "form-group" },
                                    React.createElement("input", { type: "text", className: "form-control", placeholder: "Surname", id: "registerSurName", required: true, onChange: this.handleSurName, value: this.state.UserObject.surname })),
                                React.createElement("div", { className: "form-group" },
                                    React.createElement("input", { type: "text", className: "form-control", placeholder: "Street name and house number", id: "registerStreetName", required: true, onChange: this.handleStreetName, value: this.state.UserObject.street })),
                                React.createElement("div", { className: "form-group" },
                                    React.createElement("input", { type: "text", className: "form-control", placeholder: "Postal Code", minLength: 6, maxLength: 6, id: "registerPostalCode", required: true, onChange: this.handlePostalCode, value: this.state.UserObject.postalCode })),
                                React.createElement("div", { className: "form-group" },
                                    React.createElement("input", { type: "date", className: "form-control", id: "registerDateOfBirth", required: true, onChange: this.handleDateOfBirth, value: this.state.UserObject.birthdate })),
                                React.createElement("button", { onClick: this.Register, className: "btn btn-primary" }, "Create account"))))))));
    }
}
ReactDOM.render(React.createElement(RegisterPage, null), document.getElementById("root"));
//# sourceMappingURL=RegisterPage.js.map