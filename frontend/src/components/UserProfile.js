import { Card, Container } from '@material-ui/core';
import React, { useEffect, useState } from 'react';
import { api } from '../axiosInstance';

export default function UserProfile() {
  const [user, setUser] = useState({fullname: "", userName: "", id:""});
  const token = localStorage.getItem('token');
  const getUserProfile = async () => {
    const profile = await api.get('UserProfile');
    setUser(profile);
  };
  useEffect(() => {
    getUserProfile();
  },[token]);
  return (
    <Container>
      <Card>
        <h1>hello {user.fullName}</h1>
      </Card>
    </Container>
  );
}
