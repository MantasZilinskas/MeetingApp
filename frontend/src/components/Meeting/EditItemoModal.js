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

export default function EditItemModal({
  meetingId,
  modalOpen,
  setModalOpen,
  item,
  reload,
}) {
  const [isLoading, setLoading] = useState(false);
  const { enqueueSnackbar } = useSnackbar();
  const classes = useStyles();
  const onSubmit = async (values) => {
    try {
      setLoading(true);
      const requestData = {
        name: values.name,
        description: values.description,
        deadline: new Date(values.deadline),
        userId: values.userId,
      };
      await api.put(`Meeting/${meetingId}/TodoItems/${item.id}`, requestData);
      reload(values);
      setLoading(false);
      enqueueSnackbar('To do task updated succesfuly', {
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
      initialValues={item}
    />
  );
}
