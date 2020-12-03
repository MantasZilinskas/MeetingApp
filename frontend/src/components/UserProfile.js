import { Card, Container } from '@material-ui/core';
import React, { useEffect, useState } from 'react';
import { api } from '../axiosInstance';
import { currentUserValue } from '../Utils/authenticationService';

export default function UserProfile() {
  const [user, setUser] = useState({fullname: "", userName: "", id:""});
  const currentUser = currentUserValue();
  const getUserProfile = async () => {
    const profile = await api.get('user/' + currentUser.id);
    setUser(profile);
  };
  useEffect(() => {
    getUserProfile();
  },[currentUser]);
  return (
    <Container>
      <Card>
        <h1>hello {user.fullName}</h1>
      </Card>
    </Container>
  );
}
