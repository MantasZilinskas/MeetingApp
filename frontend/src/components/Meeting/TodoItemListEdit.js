import {
  Box,
  Divider,
  IconButton,
  List,
  ListItemText,
  makeStyles,
  MenuItem,
} from '@material-ui/core';
import CloseIcon from '@material-ui/icons/Close';
import Skeleton from '@material-ui/lab/Skeleton';
import React, { useEffect, useState } from 'react';
import AddCircleIcon from '@material-ui/icons/AddCircle';
import { useParams } from 'react-router-dom';
import { api } from '../../axiosInstance';
import { useSnackbar } from 'notistack';
import CreateItemModal from './CreateItemModal';
import EditItemModal from './EditItemModal';

const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
    maxWidth: '36ch',
    backgroundColor: theme.palette.background.paper,
    maxHeight: '450px',
    minWidth: '250px',
    overflow: 'auto',
  },
  inline: {
    display: 'inline',
  },
  iconButton: {
    marginRight: theme.spacing(1),
  },
  createButton: {
    marginTop: theme.spacing(1),
  },
  dateText: {
    marginLeft: theme.spacing(1),
  },
  nemeText: {
    marginRight: theme.spacing(1),
  },
  itemLine: {
    display: 'flex',
    flexWrap: 'wrap',
  },
}));

export default function TodoItemListEdit() {
  const classes = useStyles();
  const [isLoading, setLoading] = useState();
  const [modalOpen, setModalOpen] = useState(false);
  const [meetingItems, setItems] = useState([]);
  const [isEdit, setEdit] = useState(false);
  const [editItem, setEditItem] = useState();
  const { meetingId } = useParams();
  const { enqueueSnackbar } = useSnackbar();

  const onItemClick = (item) => {
    setEditItem(item);
    setEdit(true);
    setModalOpen(true);
  };
  const reloadEdit = (item) => {
    const newList = meetingItems.filter((value) => value.id !== item.id);
    newList.push(item);
    setItems(newList);
  };
  const reloadCreate = (item) => {
    item.deadline = item.deadline.substring(0, item.deadline.indexOf('T'));
    const newList = meetingItems.slice();
    newList.push(item);
    setItems(newList);
  };

  const fetchData = async () => {
    const itemsResponse = await api.get(`meeting/${meetingId}/todoitems`);
    const response = itemsResponse.map((item) => ({
      id: item.id,
      name: item.name,
      description: item.description,
      deadline:
        item.deadline === null
          ? ''
          : item.deadline.substring(0, item.deadline.indexOf('T')),
      meetingId: item.meetingId,
      userId: item.userId,
    }));
    setItems(response);
  };
  useEffect(() => {
    fetchData();
  }, [meetingId]);

  const deleteListItem = async (item) => {
    setLoading(true);
    const newList = meetingItems.filter((element) => element.id !== item.id);
    setItems(newList);
    try {
      await api.delete(`meeting/${meetingId}/todoitems/${item.id}`);
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

    setLoading(false);
  };
  return (
    <>
      <IconButton
        color="primary"
        size="medium"
        onClick={() => {
          setEdit(false);
          setModalOpen(true);
        }}
      >
        <AddCircleIcon fontSize="large" />
      </IconButton>
      <List className={classes.root}>
        {meetingItems.map((item) =>
          isLoading ? (
            <Skeleton animation="wave" variant="rect" height={60} />
          ) : (
            <>
              <Box key={item.id} className={classes.itemLine}>
                <MenuItem alignItems="center" onClick={() => onItemClick(item)}>
                  <ListItemText
                    primary={item.name}
                    className={classes.nemeText}
                  />
                  <ListItemText
                    primary={item.deadline}
                    className={classes.dateText}
                  />
                </MenuItem>
              </Box>
              <IconButton
                onClick={() => deleteListItem(item)}
                className={classes.iconButton}
              >
                <CloseIcon />
              </IconButton>
              <Divider light />
            </>
          )
        )}
      </List>
      {isEdit ? (
        <EditItemModal
          modalOpen={modalOpen}
          setModalOpen={setModalOpen}
          meetingId={meetingId}
          item={editItem}
          reload={reloadEdit}
        />
      ) : (
        <CreateItemModal
          modalOpen={modalOpen}
          setModalOpen={setModalOpen}
          meetingId={meetingId}
          reload={reloadCreate}
        />
      )}
    </>
  );
}
