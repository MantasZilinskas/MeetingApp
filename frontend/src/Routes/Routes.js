import { Switch, Route, Redirect } from 'react-router-dom';
import SignIn from '../components/SignIn';
import SignUp from '../components/SignUp';
import UserListAdmin from '../components/UserListAdmin';

export default function Routes() {
  return (
    <Switch>
      <Route path="/signin" component={SignIn} />
      <Route path="/signup" component={SignUp} />
      <Route path="/userListAdmin" component={UserListAdmin} />
      <Route path="/">
        <Redirect to="/signin" />
      </Route>
    </Switch>
  );
}
