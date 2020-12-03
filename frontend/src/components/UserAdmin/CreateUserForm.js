import { CircularProgress, makeStyles } from '@material-ui/core';
import { useSnackbar } from 'notistack';
import React, { useState } from 'react'
import { Redirect } from 'react-router-dom';
import { api } from '../../axiosInstance';
import UserForm from './UserForm'

const useStyles = makeStyles(() => ({
    center: {
      position: 'fixed',
      top: '50%',
      left: '50%',
    },
  }));

export default function CreateUserForm() {
    const [isLoading, setIsLoading] = useState(false);
    const [redirect, setRedirect] = useState(false);
    const classes = useStyles();
    const { enqueueSnackbar } = useSnackbar();
    const onSubmit = async (values) => {
        try{
          setIsLoading(true);
          await api.post('user/register', values);
          setIsLoading(false);
          enqueueSnackbar('User was created succesfuly', {
            anchorOrigin: {
              vertical: 'bottom',
              horizontal: 'center',
            },
            variant: 'success',
          });
          setRedirect(true);
        }catch(error){
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
      const initialValues = {
        username: '',
        password: '',
        fullname: '',
        email: '',
        roles: [],
      };

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

    return (
        <UserForm onSubmit={onSubmit} initialValues={initialValues}/>
    )
}
