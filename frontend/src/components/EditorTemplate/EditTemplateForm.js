import React, { useState } from 'react';
import TemplateForm from './TemplateForm';
import { useSnackbar } from 'notistack';
import { api } from '../../axiosInstance';
import { Redirect, useParams } from 'react-router-dom';
import { CircularProgress, makeStyles } from '@material-ui/core';
import { usePromiseSubscription } from '../../Utils/usePromiseSubscription';

const useStyles = makeStyles((theme) => ({
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
}));

export default function EditTemplateForm() {
  const [isLoading, setIsLoading] = useState(true);
  const [initialValues, setInitialValues] = useState({
    name: '',
    editorText: '',
  });
  const [redirect, setRedirect] = useState(false);
  const { enqueueSnackbar } = useSnackbar();
  const { id } = useParams();
  const classes = useStyles();
  const fetchData = async () => {
    setIsLoading(true);
    var result = await api.get(`template/${id}`);
    setInitialValues(result);
    setIsLoading(false);
  };
  usePromiseSubscription(fetchData, [], [id]);
  const onSubmit = async (values) => {
    try {
      setIsLoading(true);
      await api.put(`template/${id}`, values);
      setIsLoading(false);
      enqueueSnackbar('Template was updated succesfuly', {
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

  return <TemplateForm onSubmit={onSubmit} initialValues={initialValues} />;
}
