import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import Slide from '@material-ui/core/Slide';
import {
  CircularProgress,
  Container,
  Typography,
} from '@material-ui/core';
import PropTypes from 'prop-types';
import { api } from '../../../axiosInstance';

const Transition = React.forwardRef((props, ref) => {
  Transition.displayName = 'Transtition';
  return <Slide direction="up" ref={ref} {...props} />;
});

const useStyles = makeStyles((theme) => ({
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
  container: {
    flexWrap: 'wrap',
  },
  bold: {
    fontStyle: 'bold',
  },
}));

export default function ItemModalView({ modalOpen, setModalOpen, item }) {
  const [user, setUser] = React.useState({
    id: '',
    fullName: '',
    userName: '',
  });
  const [isLoading, setLoading] = useState(false);
  const classes = useStyles();

  const handleClose = () => {
    setUser({});
    setModalOpen(false);
  };
  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      //   `meeting/${meetingId}/users/${item.userId}`
      if (item.userId) {
        const result = await api.get(`user/${item.userId}`);
        setUser(result);
      }
      setLoading(false);
    };
    fetchData();
    return clear();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [modalOpen]);
  const clear = () => {
    setUser({});
  };

  const body = (
    <>
      <Container className={classes.container}>
        <Typography variant="h3">{item.name}</Typography>
        <Typography variant="h6" className={classes.bold}>
          Description
        </Typography>
        <Typography variant="body1">{item.description}</Typography>
        <Typography variant="h6" className={classes.bold}>
          Assigned user
        </Typography>
        <Typography variant="body1">{user.fullName}</Typography>
        <Typography variant="h6" className={classes.bold}>
          Deadline
        </Typography>
        <Typography variant="body1">{item.deadline}</Typography>
      </Container>
    </>
  );
  return (
    <Dialog
      open={modalOpen}
      fullWidth={true}
      maxWidth="sm"
      scroll="body"
      TransitionComponent={Transition}
      keepMounted
      onClose={handleClose}
    >
      <DialogContent>
        {isLoading ? <CircularProgress className={classes.center} /> : body}
      </DialogContent>
    </Dialog>
  );
}
ItemModalView.propTypes = {
  modalOpen: PropTypes.bool.isRequired,
  setModalOpen: PropTypes.func.isRequired,
};
