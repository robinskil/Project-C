import * as React from 'react'
import * as ReactDOM from "react-dom";

export class Loading extends React.Component {
    constructor(props: any) {
        super(props)
        console.log("Created element");
    }
    render() {
        return (
            <div className="lds-ring">
                <div>
                </div>
                <div>
                </div>
                <div>
                </div>
                <div>
                </div>
            </div>
        )
    }
}
ReactDOM.render(
    <Loading />,
    document.getElementById("root")
);