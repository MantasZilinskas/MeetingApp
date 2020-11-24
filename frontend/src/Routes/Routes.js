import { Switch, Route } from "react-router-dom";
import SignIn from "../components/SignIn";

export default function Routes(){
    return (
        <Switch>
            <Route path="/signin" component={SignIn} />
        </Switch>
    );
}