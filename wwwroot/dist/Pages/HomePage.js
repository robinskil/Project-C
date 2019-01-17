"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const ReactDOM = require("react-dom");
class HomePage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            Loading: false
        };
    }
    render() {
        if (this.state.Loading) {
            return (React.createElement("h1", null, "Loading...."));
        }
        return (React.createElement("div", null,
            React.createElement("div", { className: "", style: { paddingTop: "10px" } },
                React.createElement("div", { className: "jumbotron", id: "banner-main" },
                    React.createElement("div", { className: "row" },
                        React.createElement("div", { className: "col-lg-3" },
                            React.createElement("img", { className: "card-img mt-3 ml-3 mb-3", src: "images/test6.jpg", alt: "No Image Available" })),
                        React.createElement("div", { className: "col-lg-9" },
                            React.createElement("h1", null, "NEW: Call of Duty: Black Ops 4"),
                            React.createElement("p", { className: "lead" }, "Call of Duty: Black Ops 4 (stylized as Call of Duty: Black Ops IIII) is a multiplayer first-person shooter developed by Treyarch and published by Activision."),
                            React.createElement("a", { className: "btn btn-light", href: "/product?name=Call of Duty Black Ops 4&platform=PS4" }, "Visit product page")))))));
    }
}
ReactDOM.render(React.createElement(HomePage, null), document.getElementById("root"));
//# sourceMappingURL=HomePage.js.map