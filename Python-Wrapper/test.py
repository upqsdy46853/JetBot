from jetbotSim import Robot, Camera
import cv2
from dqn_model import DQN
import torch
import numpy as np
import tensorflow as tf
import matplotlib.pyplot as plt
from unet_model import UNet
#from getkey import getkey, keys 
from PIL import Image
import os
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'

k = 3
action = 0
states = []
device = 'cuda'
model_path = "./best_model.dat"
seg_model_path = "./seg_model.dat"

net = DQN((3,128,128),k).to(device)
seg_model = UNet(n_channels=3, n_classes=3, bilinear=False).to(device)
net.load_state_dict(torch.load(model_path, map_location=lambda storage, loc: storage))
seg_model.load_state_dict(torch.load(seg_model_path))

def step(action):
    global robot
    if action == 0:
        robot.set_motor(1, 1)
    elif action == 1:
        robot.set_motor(0.2, 0.)
    elif action == 2:
        robot.set_motor(0., 0.2)

def execute(change):
    global states, action

    # Visualize
    img = change["new"]
    img = cv2.cvtColor(change['new'], cv2.COLOR_BGR2RGB)
    img = cv2.resize(img, (128, 128), interpolation=cv2.INTER_AREA)/255
    img = np.transpose(img, (2, 0, 1))
    seg = np.argmax(seg_model(torch.tensor(np.expand_dims(img, axis=0)).cuda().float())[0].cpu().detach().numpy(), axis=0)
    seg = seg / 2

    states.append(seg)

    if len(states) == k:
        state_v = torch.tensor([states]).to(device)
        q_vals_v = net(state_v.float())
        _, act_v = torch.max(q_vals_v, dim=1)
        action = int(act_v)
        states = []

    step(action)
    #key = getkey()
    #if key == keys.W:
    #    robot.set_motor(1, 1)
    #elif key == keys.A:
    #    robot.set_motor(0, 0.2)
    #elif key == keys.D:
    #    robot.set_motor(0.2, 0)
    if change['done']: 
        robot.reset()


robot = Robot()
camera = Camera()
camera.observe(execute)
