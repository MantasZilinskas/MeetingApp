import { Switch, Route, Redirect } from 'react-router-dom';
import SignIn from '../components/SignIn';
import UserList from '../components/UserAdmin/UserList';
import { Role } from '../Utils/Role';
import { PrivateRoute } from './PrivateRoute';
import MeetingList from '../components/Meeting/MeetingList';
import CreateUserForm from '../components/UserAdmin/CreateUserForm';
import EditUserForm from '../components/UserAdmin/EditUserForm';
import CreateMeetingDetailsForm from '../components/Meeting/CreateMeetingDetailsForm'
import EditMeetingDetailsForm from '../components/Meeting/EditMeetingDetailsForm'
import EditMeetingPage from '../components/Meeting/EditMeetingPage';
import UserMeetingList from '../components/Meeting/UserMeetingList';
import MeetingViewPage from '../components/Meeting/MeetingViewPage';
import { currentUserValue } from '../Utils/authenticationService';

export default function Routes() {
  const currentUser = currentUserValue();
  return (
    <Switch>
      <Route path="/signin" component={SignIn} />
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
        path="/meeting/create"
        roles={[Role.Moderator]}
        component={CreateMeetingDetailsForm}
      />
      <PrivateRoute
        path="/meeting/:meetingId/edit"
        roles={[Role.Moderator]}
        component={EditMeetingDetailsForm}
      />
      <PrivateRoute
        path="/meeting/:meetingId"
        roles={[Role.Moderator]}
        component={EditMeetingPage}
      />
      <PrivateRoute
        path="/meeting"
        roles={[Role.Moderator, Role.Admin]}
        component={MeetingList}
      />
       <PrivateRoute
        path="/mymeetings"
        roles={[Role.Moderator, Role.Admin, Role.StandardUser]}
        component={UserMeetingList}
      />
       <PrivateRoute
        path="/meetingview/:meetingId"
        roles={[Role.Moderator, Role.Admin, Role.StandardUser]}
        component={MeetingViewPage}
      />
      <Route path="/">
        {!currentUser && <Redirect to="/signin" />}
        {currentUser && currentUser.roles.includes(Role.Admin) && <Redirect to="/user" />}
        {currentUser && currentUser.roles.includes(Role.Moderator) && <Redirect to="/meeting" />}
        {currentUser && currentUser.roles.includes(Role.StandardUser) && <Redirect to="/mymeetings" />}
      </Route>
    </Switch>
  );
}
