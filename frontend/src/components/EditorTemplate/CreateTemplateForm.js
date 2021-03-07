import React, {useState} from 'react';
import TemplateForm from './TemplateForm';
import { useSnackbar } from 'notistack';
import { api } from '../../axiosInstance';
import { Redirect } from 'react-router-dom';
import { CircularProgress, makeStyles } from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
    center: {
      position: 'fixed',
      top: '50%',
      left: '50%',
    }
  }));

export default function CreateTemplateForm(){
    const [isLoading, setIsLoading] = useState(false);
    const [redirect, setRedirect] = useState(false);
    const { enqueueSnackbar } = useSnackbar();
    const classes = useStyles();
    const initialValues = {name: "", editorText: ""};
    const onSubmit = async (values) => {
        try {
          setIsLoading(true);
          await api.post('template', values);
          setIsLoading(false);
          enqueueSnackbar('Template was created succesfuly', {
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
      if (redirect) {
        return <Redirect to={'/template'} />;
      }
      if (isLoading) {
        return (
          <div className={classes.center}>
            <CircularProgress />
          </div>
        );
      }
    
    return <TemplateForm onSubmit={onSubmit} initialValues={initialValues}/>
}