import { CircularProgress, makeStyles } from '@material-ui/core';
import { useSnackbar } from 'notistack';
import React, { useEffect, useState } from 'react';
import { Redirect, useParams } from 'react-router-dom';
import { api } from '../../axiosInstance';
import UserForm from './UserForm';

const useStyles = makeStyles(() => ({
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
}));

export default function EditUserForm() {
  const [isLoading, setIsLoading] = useState(false);
  const [redirect, setRedirect] = useState(false);
  const [user, setUser] = useState({
    username: '',
    password: '',
    fullname: '',
    email: '',
    roles: [],
  });
  const classes = useStyles();
  const { id } = useParams();
  const { enqueueSnackbar } = useSnackbar();
  const onSubmit = async (values) => {
    try {
      setIsLoading(true);
      await api.put('user/' + id, values);
      setIsLoading(false);
      enqueueSnackbar('User was updated succesfuly', {
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'center',
        },
        variant: 'success',
      });
      setRedirect(true);
    } catch (error) {
      setIsLoading(false);
      enqueueSnackbar(error.message, {
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'center',
        },
        variant: 'error',
      });
    }
  };
  const fetchData = async () => {
    setIsLoading(true);
    const result = await api.get('user/' + id);
    setUser({
      username: result.userName,
      password: '',
      fullname: result.fullName,
      email: result.email,
      roles: result.roles,
    });
    setIsLoading(false);
  };
  useEffect(() => {
      fetchData();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  },[id]);

  if(redirect){
    return <Redirect to="/user"/>
}

  if (isLoading) {
    return (
      <div className={classes.center}>
        <CircularProgress />
      </div>
    );
  }

  return <UserForm onSubmit={onSubmit} initialValues={user} />;
}
