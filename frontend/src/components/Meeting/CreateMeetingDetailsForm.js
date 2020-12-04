import { CircularProgress, makeStyles } from '@material-ui/core';
import { useSnackbar } from 'notistack';
import React, { useState } from 'react';
import { Redirect } from 'react-router-dom';
import { api } from '../../axiosInstance';
import MeetingDetailsForm from './MeetingDetailsForm';

const useStyles = makeStyles(() => ({
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
}));

export default function CreateMeetingDetailsForm() {
  const [isLoading, setIsLoading] = useState(false);
  const [redirect, setRedirect] = useState(false);
  const [meetingId, setMeetingId] = useState(-1);
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const onSubmit = async (values) => {
    try {
      setIsLoading(true);
      const result = await api.post('meeting', values);
      setMeetingId(result.id);
      setIsLoading(false);
      enqueueSnackbar('Meeting was created succesfuly', {
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
  const initialValues = {
    name: '',
    description: '',
  };

  if (redirect) {
    return <Redirect to={'/meeting/' + encodeURI(meetingId)} />;
  }

  if (isLoading) {
    return (
      <div className={classes.center}>
        <CircularProgress />
      </div>
    );
  }

  return (
    <MeetingDetailsForm onSubmit={onSubmit} initialValues={initialValues} />
  );
}
