import * as React from "react";
import * as ReactDOM from "react-dom";
import {CheckValidityInputElement} from "../BackEndClientSide/RegisterBackend";

interface UserCredentials {
    email: string,
    password: string
}

class LoginPage extends React.Component<{}, UserCredentials> {
    constructor(props: any) {
        super(props);
        this.state = {
            email: '',
            password: ''
        };
        this.handleEmail = this.handleEmail.bind(this);
        this.handlePassword = this.handlePassword.bind(this);
    }

    handleEmail(event: any) {
        this.setState({email: event.target.value})
    }

    handlePassword(event: any) {
        this.setState({password: event.target.value})
    }

    Login() {
        console.log(this.state.email + "\t" + this.state.password);
        if(this.CheckValidation()){
            let data : FormData = new FormData();
            data.append("email" , this.state.email);
            data.append("password" , this.state.password);
            let url : string = '/User/LoginUser';
            let response = fetch(url,{
                method : "POST",
                headers: {
                    'Accept': 'application/json, application/xml, text/plain, text/html, *.*'
                },
                body : data
            });
            response.then(response => {
                if(response.ok){
                    console.log(response.text());
                    window.location.href = "/";
                }
                else {
                    console.log("Failed login");
                    //TODO : if failed return a error message
                }
            })
        }
    }
    CheckValidation() : boolean{
        return CheckValidityInputElement("loginInputEmail") && CheckValidityInputElement("loginInputPassword")
    }

    render() {
        return (
            <div style={{paddingTop:"20px"}}>
                <div className="d-flex justify-content-center">
                    <div>
                        <h3>Login</h3>
                        <hr style={{marginBottom:"8px"}}/>
                        <div className="row">
                            <div className="col-sm-12 col-md-6" style={{borderRight : "1px solid rgba(0, 0, 0, 0.1)" , marginTop: "-8.3px"}}>
                                <form style={{marginTop: "13px"}}>
                                    <div className="form-group">
                                        <input type="email" className="form-control" id="loginInputEmail" required={true} minLength={1}
                                               aria-describedby="emailHelp" placeholder="Email address"
                                               onChange={this.handleEmail} value={this.state.email} />
                                    </div>
                                    <div className="form-group">
                                        <input type="password" className="form-control" id="loginInputPassword" required={true} minLength={1}
                                               placeholder="Password" onChange={this.handlePassword} value={this.state.password}/>
                                    </div>
                                    <button type="button" onClick={() => this.Login()} className="btn btn-primary">Login
                                    </button>
                                </form>
                            </div>
                            <div className="col-sm-12 col-md-6">
                                <label style={{marginTop:"10px"}}>Don't have an account yet?</label>
                                <a style={{marginTop:"17px"}} className="btn btn-primary" href="/Register">Create a new account</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

ReactDOM.render(
    <LoginPage/>,
    document.getElementById("root")
);