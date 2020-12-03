import { Switch, Route, Redirect } from 'react-router-dom';
import SignIn from '../components/SignIn';
import SignUp from '../components/SignUp';
import UserList from '../components/UserAdmin/UserList';
import UserProfile from '../components/UserProfile';
import { Role } from '../Utils/Role';
import { PrivateRoute } from './PrivateRoute';
import MeetingList from '../components/MeetingList';
import CreateUserForm from '../components/UserAdmin/CreateUserForm';
import EditUserForm from '../components/UserAdmin/EditUserForm';

export default function Routes() {
  return (
    <Switch>
      <Route path="/signin" component={SignIn} />
      <Route path="/signup" component={SignUp} />
      <PrivateRoute
        path="/user/create"
        roles={[Role.Admin]}
        component={CreateUserForm}
      />
      <PrivateRoute
        path="/user/:id"
        roles={[Role.Admin]}
        component={EditUserForm}
      />
      <PrivateRoute
        path="/user"
        roles={[Role.Admin]}
        component={UserList}
      />
      <PrivateRoute
        path="/meeting"
        roles={[Role.Moderator, Role.Admin]}
        component={MeetingList}
      />
      <PrivateRoute path="/userProfile" component={UserProfile} />
      <Route path="/">
        <Redirect to="/signin" />
      </Route>
    </Switch>
  );
}
