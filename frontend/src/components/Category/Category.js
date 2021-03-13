import React, { useState } from 'react';
import SortableTree, {
  addNodeUnderParent,
  removeNodeAtPath,
  changeNodeAtPath,
  getFlatDataFromTree,
} from 'react-sortable-tree';
import 'react-sortable-tree/style.css';
import { makeStyles } from '@material-ui/core/styles';
import Box from '@material-ui/core/Box';
import Button from '@material-ui/core/Button';
import AddIcon from '@material-ui/icons/Add';
import IconButton from '@material-ui/core/IconButton';
import PostAddIcon from '@material-ui/icons/PostAdd';
import DeleteOutlineIcon from '@material-ui/icons/DeleteOutline';
import FiberManualRecordIcon from '@material-ui/icons/FiberManualRecord';
import TextField from '@material-ui/core/TextField';
import PropTypes from 'prop-types';
import './treeStyle.css';
//import ItemSelect from '../../../components/ItemSelect/ItemSelect';
//import ColorPickerModal from './ColorPickerModal';
import Typography from '@material-ui/core/Typography';

const useStyles = makeStyles(() => ({
  addCategoryButton: {
    alignSelf: 'flex-start',
    margin: '0 0 0 5px',
  },
  iconButton: {
    padding: 7,
  },
  instructionText: {
    fontSize: 24,
    color: '#b8b8b8',
    textAlign: 'center',
    position: 'relative',
    top: '50%',
  },
}));

function getAvailableItems(allItems, treeData, getNodeKey) {
  const assigned = getFlatDataFromTree({
    ignoreCollapsed: false,
    treeData: treeData,
    getNodeKey,
  })
    .filter((node) => node.node.type === 'item')
    .map((node) => node.node.itemId);
  const newSelectOptions = [];

  for (const item of allItems) {
    let disabled = false;
    for (const assignedItem of assigned) {
      if (item.value === assignedItem) {
        disabled = true;
        break;
      }
    }
    newSelectOptions.push({
      value: item.value,
      label: item.label,
      disabled: disabled,
    });
  }

  return newSelectOptions;
}

function ItemCategoryComponent(props) {
  const classes = useStyles();
  const getNodeKey = ({ treeIndex }) => treeIndex;
  const [isColorPickerOpen, setIsColorPickerOpen] = useState(false);
  const [currentItem, setCurrentItem] = useState({});
  const availableItems = getAvailableItems(props.items, props.treeData, getNodeKey);

  return (
    <Box height="100%" display="flex" flexDirection="column">
      <ColorPickerModal
        startColor={currentItem.node && currentItem.node.color}
        isOpen={isColorPickerOpen}
        handleClose={() => setIsColorPickerOpen(false)}
        handleChangeComplete={(color) =>
          props.onChange(
            changeNodeAtPath({
              treeData: props.treeData,
              path: currentItem.path,
              getNodeKey,
              newNode: { ...currentItem.node, color: color },
            })
          )
        }
      />
      <Box flexGrow="1">
        {!props.treeData.length && (
          <Typography className={classes.instructionText}>
            Add a new category by pressing the button below
          </Typography>
        )}
        <SortableTree
          treeData={props.treeData}
          onChange={(data) => props.onChange(data)}
          canNodeHaveChildren={(node) => node.type === 'category'}
          generateNodeProps={({ node, path }) => ({
            buttons: [
              node.type === 'category' && (
                <IconButton
                  className={classes.iconButton}
                  key="2"
                  onClick={() =>
                    props.onChange(
                      addNodeUnderParent({
                        treeData: props.treeData,
                        parentKey: path[path.length - 1],
                        expandParent: true,
                        getNodeKey,
                        newNode: {
                          itemId: null,
                          type: 'item',
                          color: '#9bceff',
                        },
                      }).treeData
                    )
                  }
                >
                  <AddIcon />
                </IconButton>
              ),
              node.type === 'category' && (
                <IconButton
                  className={classes.iconButton}
                  key="0"
                  onClick={() =>
                    props.onChange(
                      addNodeUnderParent({
                        treeData: props.treeData,
                        parentKey: path[path.length - 1],
                        expandParent: true,
                        getNodeKey,
                        newNode: {
                          title: 'Subcategory',
                          type: 'category',
                        },
                      }).treeData
                    )
                  }
                >
                  <PostAddIcon />
                </IconButton>
              ),
              node.type === 'item' && (
                <IconButton
                  className={classes.iconButton}
                  key="3"
                  onClick={() => {
                    setIsColorPickerOpen(true);
                    setCurrentItem({ node, path });
                  }}
                >
                  <FiberManualRecordIcon style={{ color: node.color }} />
                </IconButton>
              ),
              <IconButton
                className={classes.iconButton}
                key="1"
                onClick={() =>
                  props.onChange(
                    removeNodeAtPath({
                      treeData: props.treeData,
                      path,
                      getNodeKey,
                    })
                  )
                }
              >
                <DeleteOutlineIcon />
              </IconButton>,
            ],
            title: (node.type === 'category' && (
              <TextField
                value={node.title}
                onChange={(event) => {
                  const title = event.target.value;
                  props.onChange(
                    changeNodeAtPath({
                      treeData: props.treeData,
                      path,
                      getNodeKey,
                      newNode: { ...node, title },
                    })
                  );
                }}
              />
            )) || (
              <ItemSelect
                variant="standard"
                placeholder="Select item"
                fullWidth
                selectOptions={availableItems}
                value={node.itemId || 'placeholder'}
                onChange={(event) => {
                  const itemId = event.target.value;
                  props.onChange(
                    changeNodeAtPath({
                      treeData: props.treeData,
                      path,
                      getNodeKey,
                      newNode: { ...node, itemId },
                    })
                  );
                }}
              />
            ),
          })}
        />
      </Box>
      <Button
        className={classes.addCategoryButton}
        startIcon={<PostAddIcon />}
        onClick={() =>
          props.onChange(
            props.treeData.concat({
              title: 'Category',
              type: 'category',
            })
          )
        }
      >
        Add category
      </Button>
    </Box>
  );
}

ItemCategoryComponent.propTypes = {
  items: PropTypes.array,
  treeData: PropTypes.array.isRequired,
  onChange: PropTypes.func.isRequired,
};

ItemCategoryComponent.defaultProps = {
  items: [],
};

export default ItemCategoryComponent;
