import * as React from 'react'
import {CheckValidityInputElement, SetCustomValidityElement} from "../BackEndClientSide/RegisterBackend";
import User from "../Models/User";
import * as ReactDOM from "react-dom";

interface ComponentState {
    UserObject : User;
    Registering : boolean;
}

class RegisterPage extends React.Component<{}, ComponentState> {
    constructor(props: any) {
        super(props);
        this.state = {
            UserObject : new User(),
            Registering : false
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
    handleEmail(event : any){
        // this.setState({UserObject:{...this.state.UserObject,Email:event.target.value}});
        SetCustomValidityElement("registerEmail" , "");
        this.state.UserObject.email = event.target.value;
        console.log(this.state.UserObject.email);
    }
    handleFirstName(event : any){
        this.state.UserObject.name = event.target.value;
    }
    handleSurName(event : any){
        this.state.UserObject.surname = event.target.value;
    }
    handleStreetName(event : any){
        this.state.UserObject.street = event.target.value;
    }
    handlePostalCode(event : any){
        this.state.UserObject.postalCode = event.target.value;
    }
    handleDateOfBirth(event : any){
        this.state.UserObject.birthdate = event.target.value;
    }
    handlePassword(event : any){
        this.state.UserObject.password = event.target.value;
    }
    
    Register(event : any){
        //if all fields are validated
        if(this.CheckValidation()){
            event.preventDefault();
            let url : string = '/User/Register';
            let response = fetch(url,{
                method : "POST",
                headers: {
                    'Accept': 'application/json, application/xml, text/plain, text/html, *.*',
                    'Content-Type'  : 'application/json'
                },
                body : JSON.stringify(this.state.UserObject)
            });
            response.then(response => {
                if(response.ok){
                    console.log(response.text());
                    window.location.href = "/Login";
                }
                else {
                    response.json().then(
                        json => this.SetValidation(json)
                    )
                }
            })
        }
    }
    SetValidation(errors : number[]){
        for (let i = 0; i < errors.length; i++) {
            if(errors[i] == 0){
                SetCustomValidityElement("registerEmail" , "Email already exists");
            }
            else{
                SetCustomValidityElement("registerEmail" , "Something went wrong.....");
            }
        } 
    }
    
    CheckValidation(): boolean{
        return CheckValidityInputElement("registerEmail") && CheckValidityInputElement("registerPassword") && 
            CheckValidityInputElement("registerFirstName") && CheckValidityInputElement("registerSurName") &&
            CheckValidityInputElement("registerStreetName") && CheckValidityInputElement("registerPostalCode") &&
            CheckValidityInputElement("registerDateOfBirth");
    }
    
    render() {
        return (
            <div style={{paddingTop: "20px"}}>
                <div className="d-flex justify-content-center">
                    <div>
                        <h3>Create a new account</h3>
                        <hr/>
                        <div className="row">
                            <div className="col-12">
                                <form>
                                    <div className="form-group">
                                        <input type="email" className="form-control" id="registerEmail" required={true}
                                               aria-describedby="emailHelp" placeholder="Email address"
                                               onChange={this.handleEmail} value={this.state.UserObject.email}/>
                                        <small id="emailHelp" className="form-text text-muted">We'll never share your email with
                                            anyone else.
                                        </small>
                                    </div>
                                    <div className="form-group">
                                        <input type="password" className="form-control" id="registerPassword" required={true}
                                               placeholder="Password" onChange={this.handlePassword} value={this.state.UserObject.password}/>
                                    </div>
                                    <div className="form-group">
                                        <input type="text" className="form-control" placeholder="First name" id="registerFirstName"
                                               onChange={this.handleFirstName} value={this.state.UserObject.name} required={true}/>
                                    </div>
                                    <div className="form-group">
                                        <input type="text" className="form-control" placeholder="Surname" id="registerSurName" required={true}
                                               onChange={this.handleSurName} value={this.state.UserObject.surname}/>
                                    </div>
                                    <div className="form-group">
                                        <input type="text" className="form-control" placeholder="Street name and house number" id="registerStreetName" required={true}
                                               onChange={this.handleStreetName} value={this.state.UserObject.street}/>
                                    </div>
                                    <div className="form-group">
                                        <input type="text" className="form-control" placeholder="Postal Code" minLength={6} maxLength={6} id="registerPostalCode" required={true}
                                               onChange={this.handlePostalCode} value={this.state.UserObject.postalCode}/>
                                    </div>
                                    <div className="form-group">
                                        <input type="date" className="form-control" id="registerDateOfBirth" required={true}
                                               onChange={this.handleDateOfBirth} value={this.state.UserObject.birthdate}/>
                                    </div>
                                    <button onClick={this.Register} className="btn btn-primary">Create account
                                    </button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

ReactDOM.render(
    <RegisterPage/>,
    document.getElementById("root")
);