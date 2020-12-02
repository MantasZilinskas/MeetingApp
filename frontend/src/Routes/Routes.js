import { Switch, Route, Redirect } from 'react-router-dom';
import SignIn from '../components/SignIn';
import SignUp from '../components/SignUp';
import UserListAdmin from '../components/UserListAdmin';
import UserProfile from '../components/UserProfile';
import { Role } from '../Utils/Role';
import { PrivateRoute } from './PrivateRoute';
import MeetingList from '../components/MeetingList';

export default function Routes() {
  return (
    <Switch>
      <Route path="/signin" component={SignIn} />
      <Route path="/signup" component={SignUp} />
      <PrivateRoute
        path="/userListAdmin"
        roles={[Role.Admin]}
        component={UserListAdmin}
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
