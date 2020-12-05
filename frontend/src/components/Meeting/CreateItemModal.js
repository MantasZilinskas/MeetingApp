import { CircularProgress, makeStyles } from '@material-ui/core';
import { useSnackbar } from 'notistack';
import React, { useState } from 'react';
import { api } from '../../axiosInstance';
import ItemModal from './ItemModal';

const useStyles = makeStyles(() => ({
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
}));

export default function CreateItemModal({
  meetingId,
  modalOpen,
  setModalOpen,
  reload,
}) {
  const [isLoading, setLoading] = useState(false);
  const { enqueueSnackbar } = useSnackbar();
  const classes = useStyles();
  const initialValues = {
    name: '',
    description: '',
    deadline: null,
    userId: '',
  };
  const onSubmit = async (values) => {
    try {
      setLoading(true);
      const result = await api.post(`meeting/${meetingId}/todoitems`, values);
      reload(result);
      setLoading(false);
      enqueueSnackbar('To do task was created succesfuly', {
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'center',
        },
        variant: 'success',
      });
      setModalOpen(false);
    } catch (error) {
      setLoading(false);
      enqueueSnackbar(error.message, {
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'center',
        },
        variant: 'error',
      });
    }
  };
  if (isLoading) {
    return <CircularProgress className={classes.center} />;
  }
  return (
    <ItemModal
      modalOpen={modalOpen}
      setModalOpen={setModalOpen}
      onSubmit={onSubmit}
      initialValues={initialValues}
    />
  );
}
