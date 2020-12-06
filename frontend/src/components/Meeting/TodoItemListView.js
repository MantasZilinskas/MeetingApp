import {
  Box,
  Divider,
  List,
  ListItemText,
  makeStyles,
  MenuItem,
} from '@material-ui/core';
import Skeleton from '@material-ui/lab/Skeleton';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { api } from '../../axiosInstance';
import { useSnackbar } from 'notistack';
import ItemModalView from './ItemModalView';

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
  itemLine: {
    display: 'flex',
  },
}));

export default function TodoItemListView() {
  const classes = useStyles();
  const [isLoading, setLoading] = useState();
  const [modalOpen, setModalOpen] = useState(false);
  const [activeItem, setActiveItem] = useState({
    id: null,
    name: '',
    description: '',
    deadline: '',
    userId: '',
  });
  const [meetingItems, setItems] = useState([]);
  const { meetingId } = useParams();

  const onItemClick = (item) => {
    setActiveItem(item);
    setModalOpen(true);
  };

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      const itemsResponse = await api.get(`meeting/${meetingId}/todoitems`);
      const response = itemsResponse.map((item) => ({
        id: item.id,
        name: item.name,
        description: item.description,
        deadline: item.deadline.substring(0, item.deadline.indexOf('T')),
        meetingId: item.meetingId,
        userId: item.userId,
      }));
      setItems(response);
      setLoading(false);
    };
    fetchData();
  }, [meetingId]);
  return (
    <>
      <List className={classes.root}>
        {meetingItems.map((item) =>
          isLoading ? (
            <Skeleton animation="wave" variant="rect" height={60} />
          ) : (
            <Box key={item.id} className={classes.itemLine}>
              <MenuItem alignItems="center" onClick={() => onItemClick(item)}>
                <ListItemText primary={item.name} />
                <ListItemText
                  primary={item.deadline}
                  className={classes.dateText}
                />
              </MenuItem>
              <Divider light />
            </Box>
          )
        )}
      </List>
      <ItemModalView
        modalOpen={modalOpen}
        setModalOpen={setModalOpen}
        item={activeItem}
      />
    </>
  );
}
