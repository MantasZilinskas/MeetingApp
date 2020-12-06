import { CircularProgress, makeStyles } from '@material-ui/core';
import { useSnackbar } from 'notistack';
import React, { useEffect, useState } from 'react';
import { Redirect, useParams } from 'react-router-dom';
import { api } from '../../axiosInstance';
import MeetingDetailsForm from './MeetingDetailsForm';

const useStyles = makeStyles(() => ({
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
}));

export default function EditMeetingDetailsForm() {
  const [isLoading, setIsLoading] = useState(false);
  const [meeting, setMeeting] = useState({
    id: null,
    name: '',
    description: '',
    textEditorData: '',
  });
  const [redirect, setRedirect] = useState(false);
  const { meetingId } = useParams();
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const fetchData = async () => {
    setIsLoading(true);
    const result = await api.get(`meeting/${meetingId}`);
    setMeeting(result);
    setIsLoading(false);
  };
  const onSubmit = async (values) => {
    try {
      setIsLoading(true);
      const requestData = {
        name: values.name,
        description: values.description,
        textEditorData: values.textEditorData,
      };
      await api.put(`meeting/${meetingId}`, requestData);
      setIsLoading(false);
      enqueueSnackbar('Meeting details was updated succesfuly', {
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
  useEffect(() => {
    fetchData();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [meetingId]);

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

  return <MeetingDetailsForm onSubmit={onSubmit} initialValues={meeting} />;
}
