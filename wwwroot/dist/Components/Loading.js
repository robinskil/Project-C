"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const ReactDOM = require("react-dom");
class Loading extends React.Component {
    constructor(props) {
        super(props);
        console.log("Created element");
    }
    render() {
        return (React.createElement("div", { className: "lds-ring" },
            React.createElement("div", null),
            React.createElement("div", null),
            React.createElement("div", null),
            React.createElement("div", null)));
    }
}
exports.Loading = Loading;
ReactDOM.render(React.createElement(Loading, null), document.getElementById("root"));
//# sourceMappingURL=Loading.js.map